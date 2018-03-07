using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour {

	public GameObject EnemyXplosion;

	private Rigidbody2D playerRigidBody;

	public float bounceForce;



	// Use this for initialization
	void Start () {

		playerRigidBody = transform.GetComponentInParent <Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{

		if (other.tag == "Enemy") {

			//Destroy (other.gameObject);

			other.gameObject.SetActive (false);

			Instantiate (EnemyXplosion, other.transform.position, other.transform.rotation);
		
			playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, bounceForce, 0f);
		}

		if (other.tag == "Boss") {

			playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, bounceForce, 0f);
			other.transform.parent.GetComponent<Boss> ().takeDamage = true;


		}

	}



}
