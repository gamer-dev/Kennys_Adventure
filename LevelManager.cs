using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance; //to implement the singleton effect to make it inheritable by other scripts

	public float respawnWaitTime;//Time to wait for respawning

	public PlayerController thePlayer; //to make a reference to the player object

	public GameObject deathXplosion; // to refer the particle system to play upon player's death

	public int totalCoins; // to keep track of the value of the coins collected by player
	private int bonusLifeCoins;
	public int bonusLifeThreshold;
	public Text coinText;

	public AudioSource coinSound;
	public AudioSource heartSound;
	public AudioSource lifeSound;

	public Image heart1;
	public Image heart2;
	public Image heart3;

	public Sprite heartFull;
	public Sprite heartHalf;
	public Sprite heartEmpty;

	public int maxHealth;
	public int currentHealth;

	private bool isRespawning;

	public ResetOnRespawn[] objectsToReset;

	public bool isInvicible;

	public int startingLives;
	public int currentLives;

	public Text livesText;

	public GameObject gameOverScreen;
	public AudioSource gameOverMusic;

	public AudioSource levelMusic;

	public bool respawnCoActive;

	void Awake() {

		instance = this;

	}




	// Use this for initialization
	void Start () {

		thePlayer = FindObjectOfType<PlayerController> (); // find the object on the scene with the PlayerController script attached to it


		currentHealth = maxHealth;

		objectsToReset = FindObjectsOfType<ResetOnRespawn> ();

		if (PlayerPrefs.HasKey ("CoinCount")) {
		
			totalCoins = PlayerPrefs.GetInt ("CoinCount");
		}

		coinText.text = " " + totalCoins; //get the text component of the coinText and display the totalCoins value

		if (PlayerPrefs.HasKey ("PlayerLives")) {
		

			currentLives = PlayerPrefs.GetInt ("PlayerLives");
		
		} else {
		
			currentLives = startingLives;

		}


		livesText.text = "x" + currentLives;
	}
	
	// Update is called once per frame
	void Update () {

		if (currentHealth <= 0 && !isRespawning) {

			Respawn ();
			isRespawning = true;
		}

		if (bonusLifeCoins >= bonusLifeThreshold) {
		
			AddExtraLife (1);

			bonusLifeCoins -= bonusLifeThreshold;
		}

	}

	public void Respawn()
	{

		currentLives -= 1;
		livesText.text = "x" + currentLives;

		if (currentLives > 0) {
			StartCoroutine (RespawnCo ());
		} else {
			thePlayer.gameObject.SetActive (false);
			gameOverScreen.SetActive (true);
			levelMusic.Stop ();
			//levelMusic.volume = levelMusic.volume / 2f;
			gameOverMusic.Play ();

		}

	}

	public IEnumerator RespawnCo()
	{

		respawnCoActive = true;

		thePlayer.gameObject.SetActive (false); // disable the player component

		Instantiate (deathXplosion, thePlayer.transform.position, thePlayer.transform.rotation); //Instantiate (Create) the deathXplosion gameObject at the player's position and rotation

		yield return new WaitForSeconds (respawnWaitTime);//wait for respwanWaitTime seconds //THIS LINE AFFECTS HOW FAST THE CAMERA MOVEMENT OCCURS AFTER PLAYER DEATH

		respawnCoActive = false;

		currentHealth = maxHealth;

		totalCoins = 0;
		coinText.text = " " + totalCoins;
		bonusLifeCoins = 0;
		isRespawning = false;
		UpdateHeartMeter ();
		thePlayer.transform.position = thePlayer.respawnPoint; // respawn the player object at the respawn point

		thePlayer.gameObject.SetActive (true);// enable the player

		for (int i = 0; i < objectsToReset.Length; i++) {
		
			objectsToReset[i].gameObject.SetActive(true);
			objectsToReset[i].ResetObject();

		}

		//Destroy (deathXplosion);

	}

	public void AddCoins(int coinsToAdd ) //A function with parametrs, i.e takes in value, in our case our function needs a value to increase the coinCount by
	{
		totalCoins += coinsToAdd;
		bonusLifeCoins += coinsToAdd;

		coinText.text = " " + totalCoins;

		coinSound.Play ();
	}

	public void HurtPlayer(int damageToTake)
	{

		if (!isInvicible) 
		{
			
				currentHealth -= damageToTake;
				UpdateHeartMeter ();
				thePlayer.KnockBack ();
			thePlayer.hitSound.Play ();
		}
	}

	public void GiveHealth(int healthToAdd)
	{
		currentHealth += healthToAdd;

		if (currentHealth > maxHealth) {
		
			currentHealth = maxHealth;
		}

		UpdateHeartMeter ();

		heartSound.Play ();

	}

	public void UpdateHeartMeter()
	{

		switch (currentHealth) {

		case 6: 

			heart3.sprite = heartFull;
				heart2.sprite = heartFull;
				heart1.sprite = heartFull;
				return;

		case 5:
			heart3.sprite = heartHalf;
				heart2.sprite = heartFull;
				heart1.sprite = heartFull;
				return;

		case 4:
			heart3.sprite = heartEmpty;
				heart2.sprite = heartFull;
				heart1.sprite = heartFull;
				return;

		case 3:

			heart3.sprite = heartEmpty;
				heart2.sprite = heartHalf;
				heart1.sprite = heartFull;
				return;
		case 2:
			heart3.sprite = heartEmpty;
				heart2.sprite = heartEmpty;
				heart1.sprite = heartFull;
				return;
		case 1:
			heart3.sprite = heartEmpty;
			heart2.sprite = heartEmpty;
			heart1.sprite = heartHalf;
				return;
		case 0:
			heart3.sprite = heartEmpty;
			heart2.sprite = heartEmpty;
			heart1.sprite = heartEmpty;
				return;


			default:
			heart3.sprite = heartEmpty;
			heart2.sprite = heartEmpty;
			heart1.sprite = heartEmpty;
				return;
		}

	}

	public void AddExtraLife(int lifeToAdd)
	{
		currentLives += lifeToAdd;
		livesText.text = "x" + currentLives;
		lifeSound.Play ();

	}
}
