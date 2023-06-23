using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}