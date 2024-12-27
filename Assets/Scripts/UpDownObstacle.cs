using UnityEngine;

public class UpDownObstacle : MonoBehaviour
{
    public float positionUp = 0f;
    public float positionDown = 0f;
    public float speed = 2.5f;
    private bool is_up = true; 
    
    void Update()
    {
        if (is_up)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (transform.position.y >= positionUp)
            {
                is_up = false; 
            }
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y <= positionDown)
            {
                is_up = true; 
            }
        }
    }
}
