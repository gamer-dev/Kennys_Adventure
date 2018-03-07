using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

	public Sprite flagClosed; // the image to be used to show when flag is closed, i.e when the player is yet to reach the checkpoint

	public Sprite flagOpen; // the image to be shown when the flag is open, i.e the user has reached the checkpoint

	private SpriteRenderer theSpriteRenderer; // to access the spriteRenderer component of the sprite, needed to change the sprite later on

	public bool CheckPointActive; // to check if the checkpoint is active or not

	// Use this for initialization
	void Start () {

		theSpriteRenderer = GetComponent<SpriteRenderer>();// get the spriteRenderer component
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) // when the trigger occurs, if:
	{

		if (other.tag == "Player") { // an object with the tag "player" enters the triggerzone, then do:

			theSpriteRenderer.sprite = flagOpen; //replace whatever sprite is being shown with the flagOpen Sprite
			CheckPointActive = true; //set the boolean value to true, denoting that the flag has been passed by
		}

	}

}
