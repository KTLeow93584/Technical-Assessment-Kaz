using System.Collections.Generic;

using UnityEngine;

namespace Kurechii.Assessment.LeowKeanTat
{
	public class ObjectPool : MonoBehaviour
	{
		// =============================================
		#region Inspector Exposed Variables

		[Header("Core Properties")]
		[SerializeField] Transform parentTransform = null;
		[SerializeField] GameObject prefab = null;

		[SerializeField] int poolSize = 0;

		#endregion
		// =============================================
		#region Debug Variables (Read Only)

		[Header("Debug Properties")]
		[SerializeField] [ReadOnly] protected List<GameObject> activePool = new List<GameObject>();
		[SerializeField] [ReadOnly] protected List<GameObject> inactivePool = new List<GameObject>();

		#endregion
		// =============================================
		#region Helper Function (Public) - Local & External Reference Uses

		public virtual void Initialize()
		{
			// Already initialized.
			if (inactivePool.Count > 0 || activePool.Count > 0)
				return;

			GameObject[] initialPoolList = ObjectPoolDefinitions.CreateObjects(prefab, parentTransform,
				Vector3.zero, Vector3.zero, prefab.transform.localScale,
				false, prefab.name, poolSize);
			inactivePool = new List<GameObject>(initialPoolList);

			foreach (GameObject pooledObject in initialPoolList)
			{
				PooledObject objScript = pooledObject.AddComponent<PooledObject>();
				objScript.originatingPool = this;
			}
		}

		public virtual GameObject SpawnObject(Vector3 spawnPosition, Vector3 spawnRotation, Vector3 spawnScale)
		{
			GameObject spawnedPoolObject = null;

			// Pool no longer contains any freely usable objects. Dynamically expand size.
			// Lazy Load Creation. (Dynamic Object Pool Size Expansion)
			if (inactivePool.Count <= 0)
			{
				// Active Pool count is also zero. This can happen either due to setting pool size to zero or an
				// unintended behaviour from the script instance calling this function.
				if (activePool.Count == 0)
				{
					// Debug
					Debug.Log("[Object Pool - Spawn] The Object Pool for (" + prefab.name + ") is empty on both the active and inactive list. Is this intended?");
					return null;
				}

				// Active Pool count is also zero. This can happen either due to setting pool size to zero or an
				// unintended behaviour from the script instance calling this function.
				if (!prefab)
				{
					// Debug
					Debug.Log("[Object Pool - Spawn] The Object Pool for (" + prefab.name + ") needs new instances added into the pool but the prefab reference is missing.");
					return null;
				}
			}

			// Lazy Load Creation. (Dynamic Object Pool Size Expansion)
			if (inactivePool.Count <= 0)
			{
				spawnedPoolObject = ObjectPoolDefinitions.CreateObject(prefab, parentTransform,
						Vector3.zero, activePool[0].transform.rotation.eulerAngles, activePool[0].transform.localScale,
						false, prefab.name + "_" + (activePool.Count + 1));

				PooledObject objScript = spawnedPoolObject.AddComponent<PooledObject>();
				objScript.originatingPool = this;

			}
			// Spawn from Pool.
			else
				spawnedPoolObject = inactivePool[0];

			if (!spawnedPoolObject)
			{
				// Debug
				Debug.Log("[Object Pool - Spawn] Failed to spawn an instance of (" + prefab.name + ").");
				return null;
			}

			spawnedPoolObject.transform.position = spawnPosition;
			spawnedPoolObject.transform.eulerAngles = spawnRotation;
			spawnedPoolObject.transform.localScale = spawnScale;

			if (!spawnedPoolObject.gameObject.activeSelf)
				spawnedPoolObject.gameObject.SetActive(true);

			// Remove from inactive, then add to active.
			if (inactivePool.Contains(spawnedPoolObject))
				inactivePool.Remove(spawnedPoolObject);
			activePool.Add(spawnedPoolObject);

			return spawnedPoolObject;
		}

		public virtual T SpawnObjectGetComponent<T>(Vector3 spawnPosition, Vector3 spawnRotation, Vector3 spawnScale)
		{
			GameObject spawnedPoolObject = SpawnObject(spawnPosition, spawnRotation, spawnScale);
			return spawnedPoolObject.GetComponent<T>();
		}

		public virtual void ReturnToPool(GameObject instance)
		{
			if (!activePool.Contains(instance))
			{
				// Debug
				Debug.Log("[Object Pool - Despawn] This object instance (" + instance.name + ") is not part of the active pool.");
				return;
			}

			if (instance.gameObject.activeSelf)
				instance.gameObject.SetActive(false);

			// Remove from active, then add to inactive.
			activePool.Remove(instance);
			if (!inactivePool.Contains(instance))
				inactivePool.Add(instance);
			else
			{
				// Debug
				Debug.Log("[Object Pool - Despawn] The inactive pool already contains this object instance (" + instance.name + "). This is not supposed to happen.");
				return;
			}
		}

		#endregion
		// =============================================
	}
}
