using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefab;
    [SerializeField] private float spawnRangeX = 8;
    [SerializeField] private float spawnPosZ = 8;
    [SerializeField] private float startDelay = 2;
    [SerializeField] private float spawningInterval = 1.5f;
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, spawningInterval);
    }


    void SpawnObstacle()
    {
        Vector3 spawnPos = new Vector3 (Random.Range(-spawnRangeX, spawnRangeX), 1, spawnPosZ);
        int obstacleIndex = Random.Range(0,obstaclePrefab.Length);
        Instantiate(obstaclePrefab[obstacleIndex], spawnPos, obstaclePrefab[obstacleIndex].transform.rotation);

    }
}

