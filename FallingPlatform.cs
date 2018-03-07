using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

	public float fallCountdown;

	private Rigidbody2D platformRB;

	// Use this for initialization
	void Start () {

		platformRB = GetComponent<Rigidbody2D> ();

	}

	// Update is called once per frame
	void Update () {

	}

	void FallNow()
	{
		platformRB.isKinematic = false;

	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.CompareTag("Player")) {

			Invoke ("FallNow", fallCountdown);
		}

	}
}
