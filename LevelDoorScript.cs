using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoorScript : MonoBehaviour {

	public string LevelToLoad;

	public bool door_unlocked;

	public Sprite doorBottomOpen;
	public Sprite doorTopOpen;
	public Sprite doorBottomClosed;
	public Sprite doorTopClosed;

	public SpriteRenderer doorTop;
	public SpriteRenderer doorBottom;

	// Use this for initialization
	void Start () {

		PlayerPrefs.SetInt ("Level1", 1);

		if (PlayerPrefs.GetInt (LevelToLoad) == 1) {

			door_unlocked = true;
		} else {
		
			door_unlocked = false;
		}
		if (door_unlocked) {
			doorTop.sprite = doorTopOpen;
			doorBottom.sprite = doorBottomOpen;
		} else {
		
			doorTop.sprite = doorTopClosed;
			doorBottom.sprite = doorBottomClosed;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
		
			if (ETCInput.GetButtonDown ("Jump") && door_unlocked) 
			{
				SceneManager.LoadScene (LevelToLoad);
			}

		}
	}

}
