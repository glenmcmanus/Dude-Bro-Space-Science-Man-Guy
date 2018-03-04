using UnityEngine;

/* Adapted from a tutorial by Aiden Bull January 26, 2018
 * Animation integration by Glen McManus January 28, 2018
 */ 

/*
 * AAPM handles player input, providing motion to the player rigidbody2D, 
 * and triggers animation of the player's spritesheet.
 * 
 */ 
public class AAPM : MonoBehaviour {

    public AnimateSprite animateSprite;
    public Rigidbody2D rb;
	
	[SerializeField]
	private float speed = 15f;
	
	[SerializeField]
	private float jumpForce = 15f;
	
	private float horizontal;
	
	[SerializeField]
	private Transform[] groundPoints;
	
	[SerializeField]
	private float groundRadius;
    public float groundDistance = 0.01f;
	
	[SerializeField]
	private LayerMask whatIsGround;
	
	private bool isGrounded;
	
	private bool jump;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	/**
     * Reads user input and passes input to apply force to rigidbody2D
     * Handles sprite animation, flipping and offsets based on facing and input
     */ 
	void FixedUpdate () {
		horizontal = Input.GetAxis("Horizontal");
		
		isGrounded = IsGrounded();
		
		HandleMovement(horizontal);
        if(horizontal == 0 && rb.velocity == Vector2.zero)
        {
            if(animateSprite.moving == true)
                animateSprite.Idle();

        } else if(horizontal != 0)
        {
            if(animateSprite.moving == false)
                animateSprite.Move();

            if (horizontal < 0 && Player.instance.gunArm.flipY == false)
            {
                animateSprite.transform.localScale = animateSprite.spriteScaleLeft;
                Player.instance.gunArm.flipY = true;
                Player.instance.boxCollider2D.offset = Player.instance.leftFacingBoxOffset;
                Player.instance.arm.localPosition = Player.instance.shoulderMountLeft;

            } else if (horizontal > 0 && Player.instance.gunArm.flipY == true)
            {
                animateSprite.transform.localScale = animateSprite.spriteScaleRight;
                Player.instance.gunArm.flipY = false;
                Player.instance.boxCollider2D.offset = Player.instance.rightFacingBoxOffset;
                Player.instance.arm.localPosition = Player.instance.shoulderMountRight;
            }
        }
		
		if(Input.GetKey("w") && isGrounded == true){
			jump = true;

		}
		
	}
	
    /**
     * Applies force to the player's rigidbody2D based on the input direction.
     * @param horizontal    the input direction
     */ 
	private void HandleMovement(float horizontal){
		rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
		
		if(isGrounded && jump){
			isGrounded = false;
			rb.AddForce(new Vector2(0, jumpForce));
			jump = false;
		}
	}
	
    /**
     * Checks if the player is contacting the ground.
     */ 
	private bool IsGrounded(){
		if(rb.velocity.y <= 0){
            bool grounded = false;
			foreach(Transform point in groundPoints){
                RaycastHit2D hit = Physics2D.Raycast(point.position, Vector2.down, groundDistance, whatIsGround);

                if (hit.collider != null)
                    grounded = true;
			}

            return grounded;
		}
		return false;
	}
	
	
}
