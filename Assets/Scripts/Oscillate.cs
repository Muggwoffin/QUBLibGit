using UnityEngine;

public class Oscillate : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    Vector3 startPosition;
    Vector3 endPosition;
    void Start()
    {
        startPosition =  transform.position;
        endPosition = startPosition + movementVector;
    }


    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }
}

