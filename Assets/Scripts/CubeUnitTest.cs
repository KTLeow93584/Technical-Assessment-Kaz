using System.Collections.Generic;
using UnityEngine;

namespace Kurechii.Assessment.LeowKeanTat
{
	public class CubeUnitTest : MonoBehaviour
	{
		// =============================================
		#region Inspector Exposed Variables

		[Header("General Properties")]
		[SerializeField] GameObject cubeSpawnerPrefab = null;
		[SerializeField] Transform parentTransform = null;
		[SerializeField] int numberOfUnits = 0;

		#endregion
		// =============================================
		#region Debug Variables (Read Only)

		[Header("Debug Properties")]
		[ReadOnly] public List<GameObject> instanceList = new List<GameObject>();

		#endregion
		// =============================================
		#region Unity Callbacks

		void OnEnable()
		{
			// Simple creation. No need object pooling for use case.
			for (int i = 0; i < numberOfUnits; ++i)
				instanceList.Add(Instantiate(cubeSpawnerPrefab, parentTransform));
		}

		void OnDisable()
		{
			foreach (GameObject obj in instanceList)
				Destroy(obj);
			instanceList.Clear();
		}

		#endregion
		// =============================================
	}
}