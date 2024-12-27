using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class TouchOnMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LayerMask _moveLayer;
    [SerializeField] private float _moveSpeed = 5f;

    private float newSpeed = 2f;
    private float newMass = 50f;

    private float oldMass = 50f;
    private float oldSpeed = 13f; 
    private bool is_transformSphere = false;

    private bool is_transformCube = false; 
    
    private bool _isMove; 
    
    private void Start()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
		if (_rigidbody.isKinematic) {
 			_rigidbody.isKinematic = false;
		}
    }

    private void FixedUpdate()
    {
		if (_rigidbody.isKinematic){
			_rigidbody.isKinematic = false; 
		}
        _isMove = Physics.Raycast(transform.position, Vector3.forward, 0.5f, _moveLayer);

        if (!_isMove)
        {
            MoveForward();
        }
        else
        {
            StopMovement();
        }
    }

    private void MoveForward()
    {
		if (_rigidbody.isKinematic){
			_rigidbody.isKinematic = false; 
		}
 		Debug.Log($"Rigidbody isKinematic: {_rigidbody.isKinematic}");
    	Debug.Log($"Current Velocity: {_rigidbody.linearVelocity}");
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y, _moveSpeed);
    }

   private void StopMovement()
   {
	_rigidbody.isKinematic = false;
	if (_rigidbody.isKinematic)
    {
        Debug.LogError("Cannot stop movement because Rigidbody is kinematic.");
		_rigidbody.isKinematic = false; 
        return;
    }
    	_rigidbody.linearVelocity = Vector3.zero;
    	enabled = false; 
   }


    private void TransformToSphere()
    {
		if (_rigidbody.isKinematic){
			_rigidbody.isKinematic = false; 
		}

        is_transformSphere = true;

        GetComponent<MeshFilter>().mesh =
        GameObject.CreatePrimitive(PrimitiveType.Sphere).GetComponent<MeshFilter>().mesh;

        _rigidbody.mass = newMass;
        _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * newSpeed;
        
        GetComponent<BoxCollider>().enabled = false; 
        gameObject.AddComponent<SphereCollider>();
    }

    private void TransformToCube()
    {
		if (_rigidbody.isKinematic){
			_rigidbody.isKinematic = false; 
		}

        is_transformCube = true;

        GetComponent<MeshFilter>().mesh =
        GameObject.CreatePrimitive(PrimitiveType.Cube).GetComponent<MeshFilter>().mesh; 
        
        _rigidbody.mass = oldMass;
       _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * oldSpeed;
        
        GetComponent<BoxCollider>().enabled = false;
        gameObject.AddComponent<BoxCollider>();

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "TransformSphere")
        {
            TransformToSphere();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            StopMovement();
        }

        if (collision.collider.tag == "Next-Level")
        {
            StopMovement();
        }

        if (collision.collider.tag == "TransformCube")
        {
            TransformToCube();
        }
    }
    
}