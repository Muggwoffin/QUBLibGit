using UnityEngine;

public class NoiseyPatronDestroyer : MonoBehaviour
{
    public bool isRunningAway;
    [SerializeField] private float speed = 10f;
    private Vector3 runDirection;
    [SerializeField] private float xRange = 40f;
    [SerializeField] private float zRange = 40f;
    
    public PatronSpawner spawner;
    public static int hits = 0;
    public Scorer scorer;
    void Start()
    {
        scorer = FindAnyObjectByType<Scorer>();
    }
    
    void Update()
    {
        if (isRunningAway)
        {
            transform.position += runDirection * speed * Time.deltaTime;
        }
         if (transform.position.x > xRange ||  transform.position.z > zRange||
             transform.position.x <-xRange || transform.position.z < -zRange)
        {
            Destroy(gameObject);
        }
     
    }

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.ReducePatronCount();
        }
    }
    public void StartRunning(Vector3 direction)
    {

        runDirection = direction.normalized;
        isRunningAway = true;
        scorer.AddScore();
    }
}
