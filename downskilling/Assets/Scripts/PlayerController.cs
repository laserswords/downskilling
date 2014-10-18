using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	/*Movement*/
	public float maxSpeed;
	public float acceleration;
	public float jumpSpeed;
	public float translation;
	
	//Controlls
	public string horizontalControl;
	public string actionButton;
	
	
	// Update is called once per frame
	void Update () {
		//moving horizontally
		translation = Input.GetAxis (horizontalControl) * acceleration;
		translation *= Time.deltaTime;
		if (rigidbody2D.velocity.x < maxSpeed)
			rigidbody2D.AddForce(new Vector2 (translation, 0));
		
	}
}
