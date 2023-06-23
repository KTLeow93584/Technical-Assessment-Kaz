using System.Collections.Generic;

using UnityEngine;

namespace Kurechii.Assessment.LeowKeanTat
{
	public static class ObjectPoolDefinitions
	{
		// =============================================
		#region Helper Function (Public) - Spawning Object Instances

		// Single Object
		public static GameObject CreateObject(GameObject prefab, Transform parent,
			Vector3? position, Vector3? rotationEuler, Vector3? scale,
			bool startingActiveState, string instanceName)
		{
			GameObject obj = GameObject.Instantiate(prefab, parent, false);

			if (position != null)
				obj.transform.localPosition = (Vector3)position;

			if (rotationEuler != null)
				obj.transform.localRotation = Quaternion.Euler((Vector3)rotationEuler);

			if (scale != null)
				obj.transform.localScale = (Vector3)scale;

			obj.SetActive(startingActiveState);
			obj.name = (instanceName != null) ? instanceName : prefab.name;

			return obj;
		}

		public static GameObject CreateObject(GameObject prefab, Transform parent,
			Vector3? scale,
			bool startingActiveState, string instanceName)
		{
			return CreateObject(prefab, parent, null, null, scale, startingActiveState, instanceName);
		}

		public static GameObject CreateObject(GameObject prefab,
			Vector3? position, Vector3? rotationEuler, Vector3? scale,
			bool startingActiveState, string instanceName)
		{
			return CreateObject(prefab, null, position, rotationEuler, scale, startingActiveState, instanceName);
		}
		// =============================================
		// Multiple Objects
		public static GameObject[] CreateObjects(GameObject prefab, Transform parent,
			Vector3?[] position, Vector3?[] rotationEuler, Vector3?[] scale,
			bool startingActiveState, string instanceName, int count)
		{
			GameObject[] objs = new GameObject[count];

			for (int i = 0; i < count; ++i)
			{
				objs[i] = CreateObject(prefab, parent,
					(position != null) ? position[i] : Vector3.zero,
					(rotationEuler != null) ? rotationEuler[i] : Vector3.zero,
					(scale != null) ? scale[i] : Vector3.one,
					startingActiveState, instanceName);
			}

			return objs;
		}

		public static GameObject[] CreateObjects(GameObject prefab, Transform parent,
			Vector3?[] scale,
			bool startingActiveState, string instanceName, int count)
		{
			return CreateObjects(prefab, parent, null, null, scale, startingActiveState, instanceName, count);
		}

		public static GameObject[] CreateObjects(GameObject prefab,
			Vector3?[] position, Vector3?[] rotationEuler, Vector3?[] scale,
			bool startingActiveState, string instanceName, int count)
		{
			return CreateObjects(prefab, null, position, rotationEuler, scale, startingActiveState, instanceName, count);
		}

		public static GameObject[] CreateObjects(GameObject prefab, Transform parent,
			Vector3? position, Vector3? rotationEuler, Vector3? scale,
			bool startingActiveState, string instanceName, int count)
		{
			GameObject[] objs = new GameObject[count];
			for (int i = 0; i < count; ++i)
			{
				objs[i] = CreateObject(prefab, parent, position, rotationEuler, scale, startingActiveState,
					(instanceName != null) ? (instanceName + "_" + (i + 1)) : (prefab.name + "_" + (i + 1)));
			}

			return objs;
		}

		#endregion
		// =============================================
	}
}