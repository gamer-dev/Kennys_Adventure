using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string newLevel;
	public string continueLevel;
	public string creditsLevel;
	public string[] levelNames;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void NewGame()
	{
		SceneManager.LoadScene (newLevel);

		for (int i = 0; i < levelNames.Length; i++)
		{
			PlayerPrefs.SetInt (levelNames [i], 0);

		}

		PlayerPrefs.SetInt ("CoinCount", 0);
		PlayerPrefs.SetInt ("PlayerLives", 3);

	}

	public void ContinueGame()
	{
		SceneManager.LoadScene (continueLevel);
	}

	public void Credits()
	{
		SceneManager.LoadScene (creditsLevel);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}
}
