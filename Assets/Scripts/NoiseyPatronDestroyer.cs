using UnityEngine;

public class NoiseyPatronDestroyer : MonoBehaviour
{
    public bool isRunningAway;
    private Quaternion targetRotation;
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
            float turnSpeed = 100f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
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
        if (runDirection != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(runDirection, Vector3.up);
        }
        scorer.AddScore();
    }
}
