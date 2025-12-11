using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PatronSpawner : MonoBehaviour
{
  public GameObject patronPrefab;
  public GameObject player;
  private int currentCount = 0;
  [SerializeField] private int maxPatrons = 4;
  [SerializeField] private float xRange = 10.0f;
  [SerializeField] private float zRange = 10.0f;
  [SerializeField] private float spawnInterval = 3.0f;

  bool IsSpawnPointClear(Vector3 position)
  {
    float checkRadius = 1.5f;
    float patronSeperation = 5f;
    //find colliders in a sphere around the position
    Collider[] hits = Physics.OverlapSphere(position, checkRadius);

    foreach (Collider col in hits)
    {
      if (col.CompareTag("Obstacle"))
        {
        return false;
        }

      if (col.CompareTag("Patron"))
      {
        if (Vector3.Distance(position, col.transform.position) < patronSeperation)
        {
          return false;
        }
      }
    }
    return true;
  }

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
      //Attempt to find a clear spawn point
      int maxAttempts = 10;
      int attempts = 0;
      Vector3 spawnPos = Vector3.zero;
      bool foundSpot = false;

      while (attempts < maxAttempts && !foundSpot)
      {
        float randomX = Random.Range(-xRange, xRange);
        float randomZ = Random.Range(-zRange, zRange);
        spawnPos = new Vector3(randomX, 1f, randomZ);

        if (IsSpawnPointClear(spawnPos))
        {
          foundSpot = true;
        }
        else
        {
          attempts++;
        }
        
      }
      if (!foundSpot)
      {
        Debug.Log("No clear spawn area found after" + maxAttempts + " attempts");
        return;
      }

      // Make a short word 'obj' to indicate the Patron
      GameObject obj = Instantiate(patronPrefab, spawnPos, Quaternion.identity);
      NoiseyPatronDestroyer patronScript = obj.GetComponent<NoiseyPatronDestroyer>();
      
      if (patronScript != null)
      {
        // The patron is finding the player in space and pointing at the player
        Vector3 directionToPlayer = (player.transform.position - obj.transform.position);
        obj.transform.forward = directionToPlayer.normalized;
        patronScript.spawner = this;
      }
      currentCount++;
      Debug.Log("Spawn Patron Count: " + currentCount);
    }
  
}
