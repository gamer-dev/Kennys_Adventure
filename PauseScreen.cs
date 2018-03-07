using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour {

	public string levelSelect;
	public string mainMenu;

	public GameObject thePauseScreen;

	private PlayerController thePlayer;

	// Use this for initialization
	void Start () {

		thePlayer = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Pause")) 
		{
			if (Time.timeScale == 0f) {
			
				ResumeGame ();
			} else 
			{
				PauseGame ();
			}
		}
		
	}

	public void PauseGame()
	{
		Time.timeScale = 0f;

		thePauseScreen.SetActive (true);

		thePlayer.canMove = false; 

		LevelManager.instance.levelMusic.Pause ();
	}

	public void ResumeGame()
	{

		thePauseScreen.SetActive (false);

		Time.timeScale = 1f;

		thePlayer.canMove = true;

		LevelManager.instance.levelMusic.Play ();


	}

	public void LevelSelect()
	{
		PlayerPrefs.SetInt ("PlayerLives", LevelManager.instance.currentLives);
		PlayerPrefs.SetInt ("CoinCount", LevelManager.instance.totalCoins);

		Time.timeScale = 1f;

		SceneManager.LoadScene (levelSelect);

	}

	public void QuitToMenu()
	{
		Time.timeScale = 1f;

		SceneManager.LoadScene (mainMenu);
	}
}
