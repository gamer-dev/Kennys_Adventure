using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWiggleController : MonoBehaviour {

	public Transform LeftPoint;
	public Transform RightPoint;

	public float moveSpeed;

	public bool isMovingRight;

	private Rigidbody2D wiggleRigidBody;

	// Use this for initialization
	void Start () {

		wiggleRigidBody = GetComponent <Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isMovingRight && transform.position.x > RightPoint.position.x) {
		
			isMovingRight = false;


		}

		if (!isMovingRight && transform.position.x < LeftPoint.position.x) {
		
			isMovingRight = true;

		}

		if (isMovingRight) {
		
			wiggleRigidBody.velocity = new Vector3 (moveSpeed, wiggleRigidBody.velocity.y, 0f);

		} else {
			wiggleRigidBody.velocity = new Vector3 (-moveSpeed, wiggleRigidBody.velocity.y, 0f);
		}

	}
}
