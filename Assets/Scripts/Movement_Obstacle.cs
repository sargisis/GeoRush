using UnityEngine;

public class Movement_Obstacle : MonoBehaviour
{
    public float speed = 2f;
    public float positionLeft = -10f; 
    public float positionRight = 10f; 

    private bool isMovingLeft = true; 

    void Update()
    {
        // Движение влево
        if (isMovingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            // Проверяем, достиг ли объект левого предела
            if (transform.position.x <= positionLeft)
            {
                isMovingLeft = false; 
            }
        }
        // Движение вправо
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            // Проверяем, достиг ли объект правого предела
            if (transform.position.x >= positionRight)
            {
                isMovingLeft = true;
            }
        }
    }
    
}