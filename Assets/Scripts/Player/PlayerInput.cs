using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour {
	
	float jumpSpeed;
	float gravity;
	float velocitySmoothing;
	Vector3 velocity;
	PlayerController controller;

	float accel_air = 0.2f;
	float accel_ground = 0.1f;

	public float moveSpeed = 6;
	public float jumpHeight = 4;
	public float timeToJumpApex = 0.4f;

	void Start(){
		gravity = -2 * jumpHeight/Mathf.Pow (timeToJumpApex, 2);
		jumpSpeed = Mathf.Abs (gravity) * timeToJumpApex;
		controller = GetComponent<PlayerController>();
	}

	void Update(){

		if(controller.collisions.above || controller.collisions.below){
			velocity.y = 0;
		}

		Vector2 input = new Vector2(Input.GetAxisRaw ("Horizontal"),
		                            Input.GetAxisRaw ("Vertical"));

		if(Input.GetButtonDown ("Jump") && controller.collisions.below){
			velocity.y = jumpSpeed;
		}
		float targetX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetX, ref velocitySmoothing,
		                               controller.collisions.below ? accel_ground : accel_air);
		velocity.y += gravity*Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}

}
