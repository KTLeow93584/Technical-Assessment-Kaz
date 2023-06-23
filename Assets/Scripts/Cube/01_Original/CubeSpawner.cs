using UnityEngine;

namespace Kurechii.Assessment.LeowKeanTat.Original
{
	public class CubeSpawner : MonoBehaviour
	{
		public GameObject root;
		public GameObject cubePrefab;
		public float spawnInterval = 1f;
		public float spawnDuration = 60f;

		private float spawnTimer = 0f;

		private void Update()
		{
			if (spawnTimer <= spawnDuration)
			{
				spawnTimer += Time.deltaTime;

				if (spawnTimer % spawnInterval <= Time.deltaTime)
				{
					SpawnCube();
				}
			}
		}

		private void SpawnCube()
		{
			// =============================
			// Diagnostics Begin - Spawn via Creation.
			//if (DiagnosticsManager.instance != null)
			//	DiagnosticsManager.instance.StartDiagnostics();
			// =============================
			GameObject cube = Instantiate(cubePrefab, GetRandomPosition(), cubePrefab.transform.rotation);
			cube.transform.SetParent(root.transform);
			// =============================
			// Diagnostics End.
			//if (DiagnosticsManager.instance != null)
			//{
			//	DiagnosticsManager.instance.StopDiagnostics();
			//	DiagnosticsManager.instance.PrintDiagnosticResultsTicks("[Original] Game Ticks Taken to Spawn Cube: ");
			//}
			// =============================
			// Due to this being a scheduled destruction, it is unreliable to use the stopwatch as the main load will only be performed on the frame after "x" duration.
			// Profiler is the way to go here, however the difference between "Destroy" and "Deactivate via Pool" is in microseconds, it also can't be felt visibly via frame drop
			// rates or any significant changes to the Profiler's time section.
			Destroy(cube, .5f);
			// =============================
			// Diagnostics Begin - Despawn via Destroy.
			//if (DiagnosticsManager.instance != null)
			//	DiagnosticsManager.instance.StartDiagnostics();
			// =============================
			// Uncomment this for diagnostics purposes.
			//Destroy(cube);
			// =============================
			// Diagnostics End.
			//if (DiagnosticsManager.instance != null)
			//{
			//	DiagnosticsManager.instance.StopDiagnostics();
			//	DiagnosticsManager.instance.PrintDiagnosticResultsTicks("[Reworked] Game Ticks Taken to Deactivate Cube Instance: ");
			//}
			// =============================
		}

		private Vector3 GetRandomPosition()
		{
			float x = Random.Range(-5f, 5f);
			float y = Random.Range(-5f, 5f);
			float z = Random.Range(-5f, 5f);

			return new Vector3(x, y, z);
		}
	}
}
