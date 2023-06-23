using System;
using System.Diagnostics;

using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Kurechii.Assessment.LeowKeanTat
{
	public class DiagnosticsManager : MonoBehaviour
	{
		// =============================================
		#region Debug Variables (Read Only)

		[Header("Debug Properties")]
		public static DiagnosticsManager instance = null;
		Stopwatch stopWatch = null;

		#endregion
		// =============================================
		#region Unity Callbacks

		void Awake()
		{
			if (!instance)
				instance = this;
			else
				Destroy(gameObject);

			DontDestroyOnLoad(gameObject);
		}

		#endregion
		// =============================================
		#region Helper Functions (Private/Protected) - Local or Inherited Class Uses Only

		public void StartDiagnostics()
		{
			stopWatch = new Stopwatch();
			stopWatch.Start();
		}

		public void StopDiagnostics()
		{
			stopWatch.Stop();
		}

		public void PrintDiagnosticResultsFull(string context = "")
		{
			TimeSpan timespan = stopWatch.Elapsed;

			string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
				timespan.Hours, timespan.Minutes, timespan.Seconds,
				timespan.Milliseconds / 10);

			// Debug
			Debug.Log(context + "[Runtime " + elapsedTime + ", Ticks: " + stopWatch.ElapsedTicks + "].");
		}

		public void PrintDiagnosticResultsRuntime(string context = "")
		{
			TimeSpan timespan = stopWatch.Elapsed;

			string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
				timespan.Hours, timespan.Minutes, timespan.Seconds,
				timespan.Milliseconds / 10);

			// Debug
			Debug.Log(context + "[Runtime " + elapsedTime + "].");
		}

		public void PrintDiagnosticResultsTicks(string context = "")
		{
			// Debug
			Debug.Log(context + "[Ticks " + stopWatch.ElapsedTicks + "].");
		}

		#endregion
		// =============================================
	}
}
