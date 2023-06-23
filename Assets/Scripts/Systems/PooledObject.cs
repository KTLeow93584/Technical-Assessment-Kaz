using UnityEngine;

namespace Kurechii.Assessment.LeowKeanTat
{
    public class PooledObject : MonoBehaviour
	{
		// =============================================
		#region Debug Variables (Read Only)

		[Header("Debug Properties")]
		[ReadOnly] public ObjectPool originatingPool = null;

		#endregion
		// =============================================
		#region Unity Callbacks

		protected virtual void OnDisable()
		{
			// =============================
			// Diagnostics Begin - Despawn via Deactivation.
			if (DiagnosticsManager.instance != null)
				DiagnosticsManager.instance.StartDiagnostics();
			// =============================
			if (originatingPool)
				originatingPool.ReturnToPool(gameObject);
			// =============================
			// Diagnostics End.
			if (DiagnosticsManager.instance != null)
			{
				DiagnosticsManager.instance.StopDiagnostics();
				DiagnosticsManager.instance.PrintDiagnosticResultsTicks("[Reworked] Game Ticks Taken to Deactivate Cube Instance: ");
			}
			// =============================
		}

		#endregion
		// =============================================
	}
}
