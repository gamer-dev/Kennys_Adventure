using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public static CameraController instance;//to implement the singleton effect to make it inheritable

	public GameObject target; // the target to be followed by the camera

	public float followAhead;// the amount the camera is to be advanced

	private Vector3 targetPosition;// the position the camera should be in

	public float camSmooth;

	public bool camMove;

	void Awake() {

		instance = this;

	}

	// Use this for initialization
	void Start () {

		camMove = true; // a boolean to denote if the camera can move or not, used for level end animation
	}
	
	// Update is called once per frame
	void Update () {

		if (camMove) {

			targetPosition = new Vector3 (target.transform.position.x, transform.position.y, transform.position.z);// the default position the targetPosition should be in

			if (target.transform.localScale.x > 0f) { //if the player is moving right, then:

				targetPosition = new Vector3 (targetPosition.x + followAhead, targetPosition.y, targetPosition.z); // keep the y and z position same, but advance the x position value by followAhead value i.e increase the value of x axis 

			} else { // if the player is not facing right, then they are facing left, so do this:

				targetPosition = new Vector3 (targetPosition.x - followAhead, targetPosition.y, targetPosition.z);// keep the y and z position same, but decrease the x position value by followAhead value i.e lower the value of x axis 

			}

			//transform.position = targetPosition; // set the transform of the camera to that of targetPosition

			transform.position = Vector3.Lerp (transform.position, targetPosition, camSmooth * Time.deltaTime); // Lerp is used to gradually move from a value to another specified by a certain time. Time.deltaTime is used to make the animation independent of frame rate, at least that's what I understand

		}
	}
}
