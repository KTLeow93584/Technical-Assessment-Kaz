using System.Collections;

using UnityEngine;

namespace Kurechii.Assessment.LeowKeanTat.Modified
{
	public class Cube : MonoBehaviour
	{
		// =============================================
		#region Inspector Exposed Variables

		[Header("General Properties")]
		[SerializeField] public bool triggerSpawnEventOnEnable = false;
		[SerializeField] public float lifespan = 0.0f;
		Coroutine lifespanTrackerCoroutine = null;

		#endregion
		// =============================================
		#region Unity Callbacks

		void OnEnable()
		{
			if (triggerSpawnEventOnEnable)
				OnSpawn();
		}

		void OnDisable()
		{
			if (lifespanTrackerCoroutine != null)
			{
				StopCoroutine(lifespanTrackerCoroutine);
				lifespanTrackerCoroutine = null;
			}
		}

		#endregion
		// =============================================
		#region Helper Function (Public) - Local & External Reference Uses

		public void OnSpawn()
		{
			if (lifespan > 0.0f)
				lifespanTrackerCoroutine = StartCoroutine("LifespanCountdown");
		}

		#endregion
		// =============================================
		#region Helper Functions (Private/Protected) - Local or Inherited Class Uses Only

		// Use the Update() function update with an "aliveTimer" variable to track if this is required.
		IEnumerator LifespanCountdown()
		{
			if (lifespan <= 0.0f)
				yield break;
			yield return new WaitForSeconds(lifespan);

			// Self Destruct
			gameObject.SetActive(false);
		}

		#endregion
		// =============================================
	}
}