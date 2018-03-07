using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static PlayerController instance;

	public float moveSpeed; //To set the speed with which the player moves
	public float activeMoveSpeed;
	public bool canMove;

	public float jumpForce; //The speed or the "force" with which the player jumps

	public Rigidbody2D playerRigidBody; //A Rigidbody 2D to refer the player's rigidbody component

	private Animator playerAnimator;//An animator to refer to the player's animator component

	public Transform groundCheck; // A point on the screen to check 

	public float groundCheckRadius; //The radius of the point on the screen to check if it is touching the ground or not

	public LayerMask whatIsGround;// A layermask created to refer to the Ground layer, given to our ground predfabs

	bool isGrounded; // A boolean value to check if player is grounded or not 

	public Vector3 respawnPoint; //A variable to hold A location on the screen, to respawn the player in

	public GameObject stompBox;

	public float knockbackForce;//how much force should the player be knocked back by
	public float knockbackLength; //how long should the knockback last for
	private float knockbackCounter;//to count the time for knockback, starts a counter that ticks down, signalling when the knockback should stop

	public float invincibilityLength;
	private float invincibilityCounter;

	private SpriteRenderer playerSprite;

	public AudioSource jumpSound;
	public AudioSource hitSound;

	private bool onPlatform;

	public float onPlatformSpeedModifier;

	void Awake(){

		instance = this;

	}

	// Use this for initialization
	void Start () {
		canMove = true;
		activeMoveSpeed = moveSpeed;
		playerSprite = gameObject.GetComponent<SpriteRenderer> ();
		playerAnimator = GetComponent<Animator> ();// Accessess the player's animator component
		playerRigidBody = GetComponent<Rigidbody2D> (); // This line associates the player's rigidbody component to do various physics works
		respawnPoint = transform.position; // set the respawnPoint value to be the player's starting position by default

	}
	
	// Update is called once per frame
	void Update () {

		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); // check when isGrounded is True. The Overlap Circle takes in the point, creates a circle around it and checks if it is in the layer. If it is in the ground layer, then the isGrounded will be true.

		if (knockbackCounter <= 0 && canMove) {

			if(onPlatform){

				activeMoveSpeed = moveSpeed * onPlatformSpeedModifier;

			}
			else{

				activeMoveSpeed = moveSpeed;

			}




				if (ETCInput.GetButton("Right")) { // It is basically saying that if the horizontal input is greater than 0 then do this: i.e move right along +ve X axis

					playerRigidBody.velocity = new Vector3 (activeMoveSpeed, playerRigidBody.velocity.y, 0f); // playerRigidBody can access the velocity component of the player, and we set the x axis to increase by the value of moveSpeed, let the y axis be as it is and the z axis is set to be 0
					transform.localScale = new Vector3 (1f, 1f, 1f); // Set the scale component of transform of the object attached with this script to be set to default, so it appears as if it is facing right

				} else if (ETCInput.GetButton("Left")) {

					playerRigidBody.velocity = new Vector3 (-activeMoveSpeed, playerRigidBody.velocity.y, 0f);//The only thing changed here is that the x axis's value will now decrease by value of moveSpeed, so it appears to be moving left
					transform.localScale = new Vector3 (-1f, 1f, 1f); // Set the scale component of transform of the object attached with this script to be set to -1 in x field, so it appears as if it is facing left
			
				} else {
					playerRigidBody.velocity = new Vector3 (0f, playerRigidBody.velocity.y, 0f);
				}
				
				if (ETCInput.GetButtonDown ("Jump") && isGrounded) { //if the player presses jump button, and the player character is in the ground

					playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, jumpForce, 0f); //then no change in x axis, change the y axis according to the jumpforce value provided

					jumpSound.Play ();
				
				}



			//LevelManager.instance.isInvicible = false;
		}

		if (knockbackCounter > 0) {
		
			knockbackCounter -= Time.deltaTime;
			if (transform.localScale.x > 0) {
				playerRigidBody.velocity = new Vector3 (-knockbackForce, knockbackForce, 0f);
			} else {
			
				playerRigidBody.velocity = new Vector3 (knockbackForce, knockbackForce, 0f);

			}

		}

		if(invincibilityCounter>0)
			{

				invincibilityCounter -= Time.deltaTime;

			}


		if (invincibilityCounter <= 0) {
			LevelManager.instance.isInvicible = false;
			playerSprite.color = Color.white;
		}


		playerAnimator.SetFloat ("Speed", Mathf.Abs (playerRigidBody.velocity.x));// make negative values positive as we specified 0.1 in animator //i don't understand it clearly yet. NEED TO LEARN AGAIN AND THOROUGHLY!!
		playerAnimator.SetBool ("Grounded", isGrounded);//set the Grounded in Animator to our bool, isGrounded 

		if (playerRigidBody.velocity.y < 0) {
			stompBox.SetActive (true);
		} else {
		
			stompBox.SetActive (false);
		}

	}


	void OnTriggerEnter2D(Collider2D other) // when the player enters a triggerzone,
	{

		if(other.tag == "KillZone")//if the entered zone has tag "KillZone", then:
		{

			//gameObject.SetActive(false);//set the gameObject(which refers to the gameObject this script is attached to, i.e the player gameObject) to false, i.e make it completely dissapear
			//transform.position = respawnPoint; // set the player object's position to the respanPoint value

			LevelManager.instance.Respawn ();

		}


		if (other.tag == "Checkpoint") {// if the entered zone has tag "Checkpoint", then:

			respawnPoint = other.transform.position; //set the value of the respawn point to the current position of the checkpoint object

		}
	
	}


	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "MovingPlatform") {

			onPlatform = true;
			transform.parent = other.transform; // set the player's velocity to that of the moving platform, so that the player doesn't fall off the platform
		}
	}




	void OnCollisionExit2D (Collision2D other)
	{
		if (other.gameObject.tag == "MovingPlatform") {

			onPlatform = false;
			transform.parent = null;
		}
	}

	public void KnockBack() //to be called when player takes damage
	{
		knockbackCounter = knockbackLength;

		//playerSprite.color.a = 0;
		playerSprite.color = Color.red;

		invincibilityCounter = invincibilityLength;

		LevelManager.instance.isInvicible = true;
	}

}
