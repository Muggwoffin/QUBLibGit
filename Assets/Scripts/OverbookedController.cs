using System.Numerics;
using UnityEngine;

using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class OverbookedController : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float xRange = 10.0f;
    [SerializeField] private float zRange = 10.0f;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private AudioClip Spacebar;
    [SerializeField] private float shushConeAngle = 60f;
    [SerializeField] private float shushRange = 6f;
    [SerializeField] ParticleSystem shushParticles;
    
    AudioSource audioSource;
    private Rigidbody rb;
  
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("AudioSource found: " + audioSource);
    }
    void Update()
    { 
        // Capture inputs
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput =  Input.GetAxis("Vertical");
        
        //Apply movements and rotate
        
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
        float turnInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
        
        
        // Player moves left to right on the x range
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        //Player moves up and down on z
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        else if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        
        // Space bar  triggers a sound, the particles play and the patron is shushed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(Spacebar);
            shushParticles.Play();
            ShushPatrons();

        }
    }

    void ShushPatrons()
    {   // Creating a view cone that has an angle of 60 and a range 
        GameObject[] patrons = GameObject.FindGameObjectsWithTag("Patron");
        float cosThreshold = Mathf.Cos(shushConeAngle * 0.5f * Mathf.Deg2Rad);
        Vector3 forward = transform.forward;
       ;
        // Using the tag system to make the patron run away. The patron links in to the NoiseyPatronDestroyer script which interacts with the dot product calculations.
        foreach (GameObject patron in patrons)
        {
            Vector3 toPatron = (patron.transform.position - transform.position).normalized;
            Vector3 awayDirection = (patron.transform.position - transform.position).normalized;
            float dot = Vector3.Dot(forward, toPatron);
            if (dot >= cosThreshold && Vector3.Distance(transform.position, patron.transform.position) < shushRange)
            {
                Debug.Log("Patron Shushed");
                patron.GetComponent<NoiseyPatronDestroyer>().StartRunning(awayDirection);
                
            }
        }
    }
}