using UnityEngine;

namespace Kurechii.Assessment.LeowKeanTat.Modified
{
	public class CubeMovement : MonoBehaviour
	{
		// =============================================
		#region Inspector Exposed Variables

		[Header("General Properties")]
		[SerializeField] float minSpeed = 5.0f;
		[SerializeField] float maxSpeed = 5.0f;

		#endregion
		// =============================================
		#region Debug Variables (Read Only)

		[SerializeField] [ReadOnly] float currentSpeed = 5.0f;

		#endregion
		// =============================================
		#region Unity Callbacks

		protected virtual void OnEnable()
		{
			currentSpeed = Random.Range(minSpeed, maxSpeed);
		}

		protected virtual void Update()
		{
			transform.position += Vector3.right * currentSpeed * Time.deltaTime;
		}

		#endregion
		// =============================================
	}
}