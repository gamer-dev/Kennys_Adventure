using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

	public string levelSelect;
	public string mainMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Restart()
	{
		PlayerPrefs.SetInt ("CoinCount" , 0);
		PlayerPrefs.SetInt ("PlayerLives", LevelManager.instance.startingLives);

		SceneManager.LoadScene (SceneManager.GetActiveScene().name);

	}

	public void LevelSelect()
	{
		PlayerPrefs.SetInt ("CoinCount" , 0);
		PlayerPrefs.SetInt ("PlayerLives", LevelManager.instance.startingLives);

		SceneManager.LoadScene (levelSelect);
		
	}

	public void QuitToMainMenu()
	{
		SceneManager.LoadScene (mainMenu);
	}
}
