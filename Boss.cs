using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public bool bossActive;

	public float timeBetweenDrops;
	private float timeBetweenDropStore;
	private float dropCount;

	public float waitForPlatforms;
	private float platformCount;

	public Transform leftPoint;
	public Transform rightPoint;
	public Transform dropSawSpawnPoint;

	public GameObject dropSaw;

	public GameObject theBoss;

	public bool isBossRight;

	public GameObject rightPlatforms;
	public GameObject leftPlatforms;

	public bool takeDamage;

	public int StartingHealth;
	private int currentHealth;

	public GameObject levelEndPlatform;

	public CameraController theCamera;

	public bool waitingForRespawn;

	public AudioSource bossBattleMusic;

	public bool bossMusicPlay;
	// Use this for initialization
	void Start () {

		theCamera = FindObjectOfType<CameraController> ();

		timeBetweenDropStore = timeBetweenDrops;

		dropCount = timeBetweenDrops;
		platformCount = waitForPlatforms;

		theBoss.transform.position = rightPoint.position;
		isBossRight = true;

		currentHealth = StartingHealth;
	}
	
	// Update is called once per frame
	void Update () {

		if(LevelManager.instance.respawnCoActive)
		{

			bossActive = false;
			waitingForRespawn = true;
			bossBattleMusic.Stop ();
			bossMusicPlay = false;
			LevelManager.instance.levelMusic.Play ();

		}

		if (waitingForRespawn && !LevelManager.instance.respawnCoActive) 
		{

			theBoss.SetActive (false);
			rightPlatforms.SetActive (false);
			leftPlatforms.SetActive (false);

			timeBetweenDrops = timeBetweenDropStore;

			platformCount = waitForPlatforms;
			dropCount = timeBetweenDrops;

			theBoss.transform.position = rightPoint.position;
			isBossRight = true;

			currentHealth = StartingHealth;

			theCamera.camMove = true;

			waitingForRespawn = false;


		}



		if (bossActive) 
		{



			theCamera.camMove = false;
			theCamera.transform.position = Vector3.Lerp (theCamera.transform.position, new Vector3 (transform.position.x, theCamera.transform.position.y, theCamera.transform.position.z), theCamera.camSmooth*Time.deltaTime);
			//bossBattleMusic.Play ();
			theBoss.SetActive (true);




			if (dropCount > 0) {
			
				dropCount -= Time.deltaTime;
			}
			else
			{
				dropSawSpawnPoint.position = new Vector3 (Random.Range(leftPoint.position.x, rightPoint.position.x), dropSawSpawnPoint.position.y, dropSawSpawnPoint.position.z);
				Instantiate(dropSaw, dropSawSpawnPoint.position, dropSawSpawnPoint.rotation);
				dropCount = timeBetweenDrops;
			}

			if (isBossRight) {
				if (platformCount > 0) {
				
					platformCount -= Time.deltaTime;

				} else {

					rightPlatforms.SetActive(true);
				}


			} else {

				if (platformCount > 0) {

					platformCount -= Time.deltaTime;

				} else {

					leftPlatforms.SetActive(true);
				}


			}

			if (takeDamage)
			{
				
				currentHealth -= 1; 

				theCamera.camMove = true;

				if (currentHealth <= 0)
				{
					levelEndPlatform.SetActive (true);
					bossBattleMusic.Stop ();
					LevelManager.instance.levelMusic.Play ();
					gameObject.SetActive (false);

				}

				if (isBossRight) {
				
					theBoss.transform.position = leftPoint.position;

				} else {
				
					theBoss.transform.position = rightPoint.position;
				}

				isBossRight = !isBossRight;

				rightPlatforms.SetActive (false);
				leftPlatforms.SetActive (false);

				platformCount = waitForPlatforms;

				timeBetweenDrops = timeBetweenDrops / 2f;

				takeDamage = false;

			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
		
			bossActive = true;
			LevelManager.instance.levelMusic.Stop ();
			if (!bossMusicPlay) {
				bossBattleMusic.Play ();
				bossMusicPlay = true;
			}



		}
	}
}
