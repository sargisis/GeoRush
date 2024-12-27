using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    public GameManager gm; 
  	[SerializeField]  public Rigidbody rr;

    public float runSpeed = 500f; 
    public float StreafeSpeed = 500f;
    public float jumpForce = 15f;

    protected bool StreafeForward = false; 
    protected bool StreafeLeft = false;
    protected bool StreafeRight = false;
    protected bool doJump = false;

	//	Special for level_3 
    public float sudden_speed_ = 10f;
    public float sudden_speed_delay = 0.1f;
    public float sudden_speed_duration = 1.5f;
    private bool isBoost = false;
    private float boostEndTime = 0f;
    private float delayEndTime = 0f;


    public float baseJumpForce = 15f;
    private float currentJumpForce = 0f;  
    public int maxJumps = 2;          
    private int jumpCount = 0;        
    private bool isGrounded = true;  


	private float superJumpeForce = 2500f; 

	private int colorIndex = 0; 
	private Color[] superJumpColors = { Color.yellow , Color.white};
	private float colorChangeInterval = 0.5f; 
	private bool isColorChanging = false;  


    void Start() {
        currentJumpForce = baseJumpForce;
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "Obstacle") {
			gm.EndGame();
			StopCube();
        }

        if (collision.collider.tag == "Next-Level") {
			gm.NextLevel();
			StopCube(); 
        }

        if (collision.collider.tag == "Ground") {
            isGrounded = true;
            jumpCount = 0;
            currentJumpForce = baseJumpForce;
        }

        //	Level 3 
        if (collision.collider.tag == "sudden-speed") {
            sudden_speed();
        }

        if (collision.collider.tag == "super-jump") {
            isGrounded = true;
            jumpCount = 0;
            currentJumpForce = baseJumpForce;
            SuperJump();

        }
    }
    
private void Update()
    {
        if (Input.GetKey("w")) {
            StreafeForward = true;
        }
        else {
            StreafeForward = false;
        }

        if (Input.GetKey("d")) {
            StreafeRight = true;
        }
        else {
            StreafeRight = false;
        }
        
        if (Input.GetKey("a")) {
            StreafeLeft = true;
        }
        else {
            StreafeLeft = false;
        }

        if (Input.GetKeyDown("space") && (isGrounded || jumpCount < maxJumps)) {
            doJump = true;
        }

        if (transform.position.y < -5f)
        {
            gm.EndGame();

        }

        // Level 3 
        if (!isBoost && Time.time >= delayEndTime && delayEndTime > 0) {
            StartSpeedBoost();
        }

        if (isBoost) {
            if (Time.time < boostEndTime) {
                transform.position += transform.forward * sudden_speed_ * Time.deltaTime;
            }
            else {
                isBoost = false;
            }
        }
    }


private void FixedUpdate()
{
    Vector3 movement = Vector3.zero;

    if (StreafeForward) {
        movement += transform.forward * runSpeed * Time.deltaTime;
    }
    if (StreafeLeft) {
        movement += -transform.right * StreafeSpeed * Time.deltaTime;
    }
    if (StreafeRight) {
        movement += transform.right * StreafeSpeed * Time.deltaTime;
    }
    if (doJump) {
        Jump();
    }

    Vector3 targetPosition = transform.position + movement;
    targetPosition.x = 0f;
    rr.MovePosition(targetPosition); 
}


// Level 3
private void sudden_speed()
    {
        delayEndTime = Time.time + sudden_speed_delay;
    }

private void StartSpeedBoost()
    {
        isBoost = true;
        boostEndTime = Time.time + sudden_speed_duration;
    }

private void Jump()
    {
		rr.isKinematic = false; 
		
		if (rr.isKinematic) {
			Debug.Log("Kinematic is false");
			isKinematic = false; 
		}
		if (rr.veclocity.y < 0.1f)
		{
       	 rr.AddForce(Vector3.up * jumpForce * currentJumpForce, ForceMode.Impulse);
         jumpCount++;
         currentJumpForce *= 0.7f;
         isGrounded = false;
         doJump = false;
		}
    }

private void SuperJump() 
    {

        rr.AddForce(Vector3.up * superJumpeForce, ForceMode.Impulse);
        isGrounded = false;
        doJump = false;


        if (!isColorChanging) {
            isColorChanging = true;
            InvokeRepeating(nameof(ChangeColor), 0f, colorChangeInterval);
            Invoke(nameof(StopChangeColor), superJumpColors.Length * colorChangeInterval);
        }
    	
	}
private void ChangeColor() 
    {
        GetComponent<Renderer>().material.color = superJumpColors[colorIndex];
        colorIndex = (colorIndex + 1) % superJumpColors.Length;
    }

private void StopChangeColor()
    {
        CancelInvoke(nameof(ChangeColor));
        isColorChanging = false;
        ResetColor();
    }

private void ResetColor()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }

private void StopCube()
	{
		if (rr.isKinematic)
		{
			Debug.Log("Kinematic is false");
			rr.isKinematic = false; 
		}

		rr.linearVelocity = Vector3.zero;
		runSpeed = 0f;
		StreafeSpeed = 0f;
		jumpForce = 0f;
		enabled = false; 
	}
}