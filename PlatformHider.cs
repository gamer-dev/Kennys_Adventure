using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHider : MonoBehaviour {

	public float platformCountdown;
	public GameObject platformToDissapear;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator JustWait()
	{

		yield return new WaitForSeconds (platformCountdown);
		platformToDissapear.SetActive (false);


	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {

			StartCoroutine (JustWait ());
		}
	}
}
