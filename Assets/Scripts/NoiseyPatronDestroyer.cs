using UnityEngine;

public class NoiseyPatronDestroyer : MonoBehaviour
{
    public bool isRunningAway;
    private Quaternion targetRotation;
    [SerializeField] private float speed = 10f;
    private Vector3 runDirection;
    [SerializeField] private float xRange = 40f;
    [SerializeField] private float zRange = 40f;
    [SerializeField] private float obstacleCheckDistance = 8f;
    [SerializeField] private float avoidTurnSpeed = 75f;
    [SerializeField] private string obstacleTag = "Obstacle";
    
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
            AvoidObstacle();
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

    public void AvoidObstacle()
    {
        //if we are not running anywhere we don't need to check anything.
        if (runDirection == Vector3.zero) return;
        
        //Build a ray from the patron position, forward in runDirection
        Ray ray = new Ray(transform.position, runDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, obstacleCheckDistance))
        { if (hit.collider != null && hit.collider.CompareTag(obstacleTag))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
                //Define a Vector3 that makes the patron move along shelving rather than through it.
                Vector3 avoidDirection = Vector3.Cross(hit.normal, Vector3.up).normalized;
                //Choose the side that is closest to the patron to move 
                if (Vector3.Dot(avoidDirection, runDirection) < 0)
                {
                    avoidDirection = -avoidDirection;
                }
                //Steer runDirection towards that side
                runDirection = Vector3.RotateTowards(runDirection, avoidDirection,
                    avoidTurnSpeed * Mathf.Deg2Rad * Time.deltaTime, 0f);
                //Update visual so the patron also turn in the correct direction, linking into lookrotation
                if (runDirection != Vector3.zero)
                {
                    targetRotation = Quaternion.LookRotation(runDirection, Vector3.up);
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * obstacleCheckDistance, Color.green);
            }
        }

        else
        {
            Debug.DrawRay(ray.origin, ray.direction * obstacleCheckDistance, Color.green);
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
