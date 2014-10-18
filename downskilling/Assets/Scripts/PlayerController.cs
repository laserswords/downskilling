using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	/*Movement*/
	public float maxSpeed;
	public float acceleration;
	public float jumpSpeed;
	public float translation;
	public float angle;

	//bools
	public bool jump;
	
	//Controlls
	public string horizontalControl;
	public string jumpButton;
	//public KeyCode jumpButton;
	
	// Update is called once per frame
	void Update () {
		//moving horizontally
		translation = Input.GetAxis (horizontalControl) * acceleration;
		translation *= Time.deltaTime;
		if (rigidbody2D.velocity.x < maxSpeed) {
			rigidbody2D.AddForce(new Vector2 (translation, 0));
		}
		
		//Jump
		if (Input.GetButtonDown (jumpButton) && jump) {
			jump = false;
			Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
			rigidbody2D.AddForce(dir * jumpSpeed);
		}		
	}

	void OnCollisionEnter(Collision col) {

	}
}
