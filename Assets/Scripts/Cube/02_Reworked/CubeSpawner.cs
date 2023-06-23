using UnityEngine;

namespace Kurechii.Assessment.LeowKeanTat.Modified
{
	// =============================================
	public class CubeSpawner : MonoBehaviour
	{
		// =============================================
		#region Inspector Exposed Variables

		[Header("Core Components")]
		[SerializeField] ObjectPool poolScript = null;
		[SerializeField] Camera viewCamera = null;

		[Header("Adjustable Properties")]
		[Tooltip("In-Between Duration")]
		[SerializeField] float spawnInterval = 1.0f;

		[Tooltip("Total Duration Before Spawner Stops. Negative Value = Infinite Spawn Duration")]
		[SerializeField] float spawnDuration = 60.0f;

		[SerializeField] float minCubeSize = 0.0f;
		[SerializeField] float maxCubeSize = 0.0f;

		#endregion
		// =============================================
		#region Debug Variables (Read Only)

		[Header("Debug Properties")]
		[SerializeField] [ReadOnly] float spawnTimer = 0.0f;

		#endregion
		// =============================================
		#region Unity Callbacks

		void Awake()
		{
			if (!viewCamera)
				viewCamera = Camera.main;

			if (!poolScript)
				poolScript = GetComponent<ObjectPool>();
		}

		void OnEnable()
		{
			poolScript.Initialize();
		}

		void Update()
		{
			spawnTimer += Time.deltaTime;
			if (spawnTimer >= spawnInterval)
			{
				spawnTimer -= spawnInterval;
				SpawnCube();
			}

			if (spawnDuration > 0.0f && spawnTimer >= spawnDuration)
				this.enabled = false;
		}

		#endregion
		// =============================================
		#region Helper Functions (Private/Protected) - Local or Inherited Class Uses Only

		void SpawnCube()
		{
			// =============================
			// Diagnostics Begin - Spawn via Object Pooling.
			//if (DiagnosticsManager.instance != null)
				//DiagnosticsManager.instance.StartDiagnostics();
			// =============================
			poolScript.SpawnObject(GetRandomPosition(), GetRandomRotation(), GetRandomSize());
			// =============================
			// Diagnostics End.
			//if (DiagnosticsManager.instance != null)
			//{
				//DiagnosticsManager.instance.StopDiagnostics();
				//DiagnosticsManager.instance.PrintDiagnosticResultsTicks("[Reworked] Game Ticks Taken to Spawn Cube: ");
			//}
			// =============================
		}

		Vector3 GetRandomPosition()
		{
			float x = Random.Range(-5.0f, 5.0f);
			float y = Random.Range(-5.0f, 5.0f);
			float z = Random.Range(35.0f, 55.0f);

			return viewCamera.transform.position + new Vector3(x, y, z);
		}

		Vector3 GetRandomRotation()
		{
			float x = Random.Range(0.0f, 360.0f);
			float y = Random.Range(0.0f, 360.0f);
			float z = Random.Range(0.0f, 360.0f);

			return new Vector3(x, y, z);
		}

		Vector3 GetRandomSize()
		{
			float resultantCubeSize = Random.Range(minCubeSize, maxCubeSize);
			return new Vector3(resultantCubeSize, resultantCubeSize, resultantCubeSize);
		}

		#endregion
		// =============================================
	}

}