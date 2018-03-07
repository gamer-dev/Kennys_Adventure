using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public int coinValue; //the value that we want our specific coin to be (gold or silver?)


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
		
		

			//Destroy (gameObject);

			gameObject.SetActive (false);

			LevelManager.instance.AddCoins (coinValue);

			 //destroy the gameobject the script is attached to, if a gamobject with tag "player" touches it



		}

	}




}
