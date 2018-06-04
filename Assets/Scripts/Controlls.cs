using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlls : MonoBehaviour {
	public float playerSpeed = 20.0f;
	public float jumpPower = 330.0f;
	public bool playerOnGround;
	public LayerMask ground;
	public float maxSpeed = 8.0f;

	private GameObject AudioSource;
	private Animator animator;
	private Rigidbody2D rgbd2d;
	private SpriteRenderer sr;
	private bool canDoubleJump = true;
	private MusicControll musicControll;

	public float knockback;
	public float knockbackLength;
	public float knockbackCount;
	public bool knockFromRight;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		rgbd2d = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		AudioSource = GameObject.FindGameObjectWithTag("AudioSource");
		musicControll = AudioSource.GetComponent<MusicControll>();
		//bc2d = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {

		if (knockbackCount <= 0){
		// Move Player if Directional Input is pressed
		float inputSpeed = Input.GetAxisRaw("Horizontal");

		// Check if Maximum speed reached
		if(rgbd2d.velocity.x < maxSpeed && rgbd2d.velocity.x > -1*maxSpeed){
			rgbd2d.AddForce(new Vector2(playerSpeed * inputSpeed,0));
		}
		// Turn the Sprite around when changing Directions, but leave it as is if no input (speed = 0)
		if(inputSpeed < 0 ){
			sr.flipX = true;
		}
		if(inputSpeed > 0){
			sr.flipX = false;
		}
		
		// Stop moving when Keys released on Ground
		if(playerOnGround && inputSpeed == 0){
			stopHorizontalMovement();
		}

		if(rgbd2d.velocity.x < 0 && inputSpeed > 0 || rgbd2d.velocity.x > 0 && inputSpeed < 0){
			stopHorizontalMovement();
		}

		// Jump on Up Arrow
		if(Input.GetKeyDown(KeyCode.UpArrow) && playerOnGround){
			Jump();
		}

		// Double Jump Check
		if(Input.GetKeyDown(KeyCode.UpArrow) && !playerOnGround && canDoubleJump){
			// Set Vertical Velocity to 0 to jump at maximum Power ignoring gravit
			rgbd2d.velocity = new Vector2(rgbd2d.velocity.x, 0);
			Jump();
			canDoubleJump = false;
		}
		
		
		

		// Check if player hits the ground and change values
		RaycastHit2D hit = Physics2D.Raycast(transform.position,new Vector2(0,-1.1f),1.1f,ground);
		if((hit.collider != null) == true){
			playerOnGround = true;
			canDoubleJump = true;
		}
		else {
			playerOnGround = false;
		}

		// Handle Animation on Ground
		if(playerOnGround){
			if(inputSpeed != 0){
				animator.SetInteger("state",1);
			}
			else {
				animator.SetInteger("state",0);
			}
		}
		
		// Handle Animation mid-air
		if(!playerOnGround){
			if(rgbd2d.velocity.y > 0){
				// Player is going up
				animator.SetInteger("state",2);
			}
			else {
				// Player is falling
				animator.SetInteger("state",3);
			}
		}

		//handleBoxCollider();
		
	}
	else {
		if(knockFromRight)
		rgbd2d.velocity = new Vector2(-knockback,knockback);
		if(!knockFromRight)
		rgbd2d.velocity = new Vector2(knockback,knockback);
		knockbackCount -= Time.deltaTime;
		}
	}

	void Jump(){
		musicControll.playJumpSound();
		rgbd2d.AddForce(new Vector2(0,jumpPower));
	}

	void stopHorizontalMovement(){
			rgbd2d.velocity = new Vector2(0, rgbd2d.velocity.y);
	}

	// Change Bounds of BoxCollider in Air to better fit the Sprite
	/*
	void handleBoxCollider(){
		if(!playerOnGround){
			bc2d.offset = new Vector2(bc2d.offset.x,-0.01758363f);
			bc2d.size = new Vector2(0.1326492f,0.1689075f);
		}
		else {
			bc2d.offset = new Vector2(bc2d.offset.x,-0.04656493f);
			bc2d.size = new Vector2(0.1326492f,0.2268701f);
		}
	}
	*/
}
