using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

	public float particleLifeTime; // a float value to denote the lifetime of the particle

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		particleLifeTime = particleLifeTime - Time.deltaTime; //decrease the particle lifetime value by a random number each frame

		if (particleLifeTime <= 0f) { //when the lifecycle time is less than or equal to 0

			Destroy (gameObject); //destroy the particle

		}

	}
}
