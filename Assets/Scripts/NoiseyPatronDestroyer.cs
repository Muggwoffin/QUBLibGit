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
    {// make sure the scorer is connected here so that the score goes up when patron is shushed
        scorer = FindAnyObjectByType<Scorer>();
    }
    
    void Update()
    {
        if (isRunningAway)
        {
            // running away causes patron to run away and also gradually rotate rather than snap rotating
            transform.position += runDirection * speed * Time.deltaTime;
            float turnSpeed = 100f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
            // Patron is destroyed once they head beyond a certain range
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
        // Some fancy maths going on here that causes the rotation of the Patron to be smoother.
        if (runDirection != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(runDirection, Vector3.up);
        }
        scorer.AddScore();
    }
}
