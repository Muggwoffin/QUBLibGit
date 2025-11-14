using UnityEngine;
using UnityEngine.Rendering;

public class PatronSpawner : MonoBehaviour
{
  public GameObject patronPrefab;
  private int currentCount = 0;
  [SerializeField] private int maxPatrons = 4;
  [SerializeField] private float xRange = 10.0f;
  [SerializeField] private float zRange = 10.0f;
  [SerializeField] private float spawnInterval = 3.0f;

  public void ReducePatronCount()
  {
    currentCount--;
  }
  void Start()
  {
    InvokeRepeating(nameof(SpawnPatron), 0f, spawnInterval);
  }
    void SpawnPatron()
    {
      if (currentCount >= maxPatrons) return;
      float randomX = Random.Range(-xRange, xRange);
      float randomZ = Random.Range(-zRange, zRange);
      Vector3 spawnPos = new Vector3(randomX, 1f, randomZ);
      GameObject obj = Instantiate(patronPrefab, spawnPos, Quaternion.identity);
      NoiseyPatronDestroyer patronScript = obj.GetComponent<NoiseyPatronDestroyer>();
      if (patronScript != null)
      {
        patronScript.spawner = this;
      }
      currentCount++;
      Debug.Log("Spawn Patron Count: " + currentCount);
    }
  
}
