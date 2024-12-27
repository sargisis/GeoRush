using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class JumpOnTouch : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private LayerMask _groundLayer;

    private bool _isGrounded;

    private void Update()
    {
       
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.6f, _groundLayer);

        if (Input.GetMouseButtonDown(0) && _isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
 
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, _jumpForce, _rigidbody.linearVelocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            StopJump();
        }

        if (collision.collider.CompareTag("Next-Level"))
        {
            StopJump();
        }
    }

    private void StopJump()
    {
  
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.isKinematic = true; 
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle")) 
        {
            _rigidbody.isKinematic = false;
        }

        if (collision.collider.CompareTag("Next-Level"))
        {
            _rigidbody.isKinematic = false; 
        }
    }
}