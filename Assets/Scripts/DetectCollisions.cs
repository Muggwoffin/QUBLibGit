
using System;
using Unity.Cinemachine;
using UnityEngine;

public class DetectCollisions : MonoBehaviour


{
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip failure;
    [SerializeField] AudioClip shoot;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem failParticles;

    public static int hits = 0;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("AudioSource found: " + audioSource);
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                FriendlySequence();
                break;
            case "Enemy":
                EnemySequence();
                break;
            case "Projectile":
                audioSource.PlayOneShot(shoot);
                Destroy(other.gameObject);
                break;
            default:
                break;
        }
        void FriendlySequence()
            {
            audioSource.PlayOneShot(success);
            Scoring(other.gameObject.tag);
            FindObjectOfType<Scorer>().AddScore();
            Instantiate(successParticles, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            }
        void EnemySequence()
            {
                Instantiate(failParticles, other.transform.position, Quaternion.identity);
                Scoring(other.gameObject.tag);
                FindObjectOfType<Scorer>().SubtractScore();
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
    }
    // Scoring goes up 1 when Friendly is Hit and down 1 when Enemy is hit
    
    private void Scoring(string tag)
    {
        if (tag == "Friendly")
        {
            hits++;
            Debug.Log("Score: " + hits);
        }
        else if (tag == "Enemy")
        {
            hits--;
            Debug.Log("Score: " + hits);
        }

    }
}
