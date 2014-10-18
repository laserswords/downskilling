using UnityEngine;
using System.Collections;

public class XboxController : MonoBehaviour {

	/*Movement*/
	public float maxSpeed;
	public float acceleration;
	public float jumpSpeed;
	public float translation;

	//Abilities
	public bool jump;
	public bool glide;
	public bool flip;

	//Controlls
	public string horizontalControl;
	public string actionButton;

	//SoundFx
	public AudioClip jumpSFX;
	public AudioClip landSFX;
	public AudioClip glideSFX;
	public AudioClip flipSFX;
	public AudioClip deathSFX;
	public AudioClip winSFX;

	//Components
	SpriteRenderer spriteRenderer;
	TrailRenderer trailRenderer;

	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		trailRenderer = this.GetComponent<TrailRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		//moving horizontally
		translation = Input.GetAxis (horizontalControl) * acceleration;
		translation *= Time.deltaTime;
		if (rigidbody2D.velocity.x < maxSpeed)
			rigidbody2D.AddForce(new Vector2 (translation, 0));

		//Action
		if (Input.GetButtonDown(actionButton) || Input.GetAxis (actionButton) > 0) {
			Action ();
		}

		//gliding
		if (glide) {
			if (!flip) {
				if (rigidbody2D.velocity.y < 0)
					rigidbody2D.gravityScale = 0.25f;
			    if (rigidbody2D.velocity.y < -3)
					rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, -3);
			} else {
				if (rigidbody2D.velocity.y > 0)
					rigidbody2D.gravityScale = -0.25f;
				if (rigidbody2D.velocity.y > 3)
					rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 3);
			}
		}

		//restarting
		if (Input.GetButtonDown("Fire2"))
		    Application.LoadLevel(Application.loadedLevel);
	}


	void OnCollisionEnter2D (Collision2D other) {
		//reload level if fail
		if (other.gameObject.tag == "obstacle") {
			MusicScript.audioSource.PlayOneShot(deathSFX);
			Application.LoadLevel(Application.loadedLevel);
		}else if (other.gameObject.tag == "platform") {
			MusicScript.audioSource.PlayOneShot(landSFX, 0.5f);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		//change color of pixel
		if (other.tag != "flip") {
			ChangeColor (other.gameObject);
			ClearAbilities ();
		}

		//ability manager
		if (other.tag == "jump") {
			jump = true;
		}else if (other.tag == "glide") {
			glide = true;
			MusicScript.audioSource.PlayOneShot(glideSFX, 0.5f);
		}else if (other.tag == "flip") {
			flip = !flip;
			jumpSpeed *= -1;
			rigidbody2D.gravityScale *= -1;
			MusicScript.audioSource.PlayOneShot(flipSFX, 0.5f);
		}
		//win manager
		if (other.tag == "goal") {
			MusicScript.audioSource.PlayOneShot(winSFX);
			if (Application.levelCount > Application.loadedLevel + 1) {
				Application.LoadLevel(Application.loadedLevel + 1);
			} else {
				//start from beginning
				Application.LoadLevel(0);
			}
		}

		///////////////////////////////////
		//SHOULD DEAL WITH OBJECT TOUCHED//
		///////////////////////////////////
		Destroy (other.gameObject);
	}

	void Action () {
		if (jump) {
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0);
			rigidbody2D.AddForce(new Vector2 (0, jumpSpeed));
			MusicScript.audioSource.PlayOneShot(jumpSFX);
		}
		if (!glide) {
			ClearAbilities ();
			ClearColor ();
		}
	}

	void ClearAbilities () {
		if (Mathf.Abs(rigidbody2D.gravityScale) == 0.25f) {
			rigidbody2D.gravityScale *= 4;
		}
		jump = false;
		glide = false;
	}

	void ClearColor () {
		spriteRenderer.color = Color.black;
		ChangeTrailColor (Color.black);
	}

	void ChangeColor (GameObject other) {
		Color newColor = other.GetComponent<SpriteRenderer> ().color;
		spriteRenderer.color = newColor;
		ChangeTrailColor (newColor);
	}

	void ChangeTrailColor (Color newColor) {
		trailRenderer.material.color = newColor;
	}
}
