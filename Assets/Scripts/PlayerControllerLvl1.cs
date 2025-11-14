using UnityEngine;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    private float horizontalInput;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float xRange = 10.0f;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private AudioClip Spacebar;
    
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("AudioSource found: " + audioSource);
    }
    void Update()
    { // Player moves left to right on the x range
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        // Space bar shoots a projectile and triggers a sound
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player Position: " + transform.position);
            Debug.Log("SpawnPoint World Position: " + spawnPoint.position);
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            audioSource.PlayOneShot(Spacebar);   
            Debug.Log("Projectile Instantiated World Position: " + projectile.transform.position);
        }
    }
}