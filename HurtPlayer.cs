using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

	public int damageToGive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		


	}

	void OnTriggerEnter2D (Collider2D other)
	{

		if(other.tag == "Player")//if an object with the tag player has entered the TriggerZone, then:
		{
			
			//LevelManager.instance.Respawn ();// call the Respawn() function from the LevelManager Script 

			LevelManager.instance.HurtPlayer (damageToGive); //call the hurtplayer function and pass on a value for it to damage the player
		
		}
		 

	}

}
