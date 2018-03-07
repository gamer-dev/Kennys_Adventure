using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour {

	public float moveSpeed;
	public bool canMove;

	private Rigidbody2D spiderRigidBody;

	void Start () {

		canMove = false;

		spiderRigidBody = GetComponent<Rigidbody2D> ();

	}
	
	void Update () {

		if (canMove) {
			
			spiderRigidBody.velocity = new Vector3(-moveSpeed, spiderRigidBody.velocity.y, 0f);
		
		}
	}

	public void OnBecameVisible()
	{

		canMove = true;

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "KillZone") {

			//Destroy (gameObject);

			gameObject.SetActive (false);
		}
	}


	void OnEnable()
	{
		canMove = false;
	}
}
