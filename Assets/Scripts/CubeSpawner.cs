using UnityEngine;

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
        GameObject cube = Instantiate(cubePrefab, GetRandomPosition(), cubePrefab.transform.rotation);
        cube.transform.SetParent(root.transform);
        Destroy(cube, 5f);
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);
        float z = Random.Range(-5f, 5f);

        return new Vector3(x, y, z);
    }
}
