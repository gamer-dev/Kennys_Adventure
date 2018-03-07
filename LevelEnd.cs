using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

	public string levelToLoad; // a string vaue for typing in the scene to be loaded, which is always set to levelSelection menu
	public string LevelToUnlock; // a string vaue for typing in the scene to be unlocked

	public Sprite flagClosed; // sprite to be displayed when the flag is closed
	public Sprite flagOpen; // sprite to be displayed when the flag is open

	public float waitToMove; //time value to wait before player can move
	public float waitToLoad; //time value to wait before next level is loaded

	private SpriteRenderer theSpriteRenderer; // a SpriteRenderer variable to access the SpriteRenderer component of the object and to change the sprite later on

	private bool movePlayer;

	public AudioSource levelEndMusic;

	// Use this for initialization
	void Start () {

		theSpriteRenderer = GetComponent<SpriteRenderer> (); // to access the SpriteRenderer component of the object, for the purpose of changing sprites
	}
	
	// Update is called once per frame
	void Update () {

		if (movePlayer) {
		
			PlayerController.instance.playerRigidBody.velocity = new Vector3 (PlayerController.instance.moveSpeed, PlayerController.instance.playerRigidBody.velocity.y, 0f);
		
		}


	}

	void OnTriggerEnter2D(Collider2D other)
	{

		theSpriteRenderer.sprite = flagOpen;

		StartCoroutine ("LevelEndCo");


		// change the sprite to denote that the flag has been opened
		//SceneManager.LoadScene (levelToLoad);  // load the level that should occur next
	
	}

	public IEnumerator LevelEndCo()
	{
		PlayerController.instance.canMove = false;
		CameraController.instance.camMove = false;

		LevelManager.instance.levelMusic.Stop ();
		levelEndMusic.Play ();

		PlayerController.instance.playerRigidBody.velocity = Vector3.zero;

		PlayerPrefs.SetInt ("CoinCount", LevelManager.instance.totalCoins);
		PlayerPrefs.SetInt ("PlayerLives", LevelManager.instance.currentLives);

		PlayerPrefs.SetInt (LevelToUnlock, 1);

		yield return new WaitForSeconds (waitToMove);

		movePlayer = true;

		yield return new WaitForSeconds (waitToLoad);

		SceneManager.LoadScene (levelToLoad);
	}
}
