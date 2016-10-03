using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//movement variables
	public float maxSpeed;

	//Jumping Variables
	bool grounded = false;
	float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpHeight;


	Rigidbody2D myRB;
	Animator myAnim;
	bool facingRight;

	// For Shooting
	public Transform starTip;
	public GameObject star;
	float fireRate = 0.2f;
	float nextFire = 0f;

	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();

		facingRight = true;
	
	}
		
	
	// Update is called once per frame
	void Update(){
		//if (grounded && Input.GetAxis ("Jump") > 0) {
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			grounded = false;
			myAnim.SetBool ("isGrounded", grounded);
			myRB.AddForce (new Vector2 (0, jumpHeight));
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			myAnim.SetBool ("Crouch", true);
			maxSpeed = 0;
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			myAnim.SetBool ("Crouch", false);
			maxSpeed = 5;
		}

		// Player Shooting
		//if(Input.GetAxisRaw("Fire1")>0) fireStar(); <-- this is for fire constant while hold keydown
		if(Input.GetKeyDown(KeyCode.LeftControl))fireStar();
	}
				
	void FixedUpdate () {

		// Check if we are grounded if no then we are falling
		grounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,groundLayer);
		myAnim.SetBool ("isGrounded", grounded);


		myAnim.SetFloat ("verticalSpeed", myRB.velocity.y);

		float move = Input.GetAxis ("Horizontal");
		myAnim.SetFloat ("Speed", Mathf.Abs (move));

		myRB.velocity = new Vector2 (move * maxSpeed, myRB.velocity.y);

		if (move > 0 && !facingRight) {
			flip ();
		} else if (move < 0 && facingRight) {
			flip ();
		}
	}

	void flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	void fireStar(){
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			if (facingRight) {
				Instantiate (star, starTip.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
			} else if (!facingRight) {
				Instantiate (star, starTip.position, Quaternion.Euler (new Vector3 (0, 0, 180f)));
			}
		}
	}
}
