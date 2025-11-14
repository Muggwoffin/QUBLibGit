using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField] private float ZtopBound = 100;
    [SerializeField] private float ZlowerBound = -100;
    [SerializeField] private float XtopBound = 100;
    [SerializeField] private float XlowerBound = -100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.z > ZtopBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < ZlowerBound)
        {
            Destroy(gameObject);
        } 
        
               
        if (transform.position.x > XtopBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < XlowerBound)
        {

            Destroy(gameObject);
        } 
    }
}
