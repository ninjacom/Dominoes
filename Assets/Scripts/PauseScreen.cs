using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour {

	Image ControlsImage;
	public Sprite sVR, sKeyboard;
	bool isPaused;
	public AudioClip Pause;
	public AudioClip Unpause;
	AudioSource audio;
	GameLogic2 GameLogicScript;

	void Awake () {

		GameLogicScript = GameObject.Find ("GameController").GetComponent<GameLogic2> ();
		GameLogicScript.GhostOn = true;

	}

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		isPaused = false;
		ControlsImage = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!isPaused && Input.GetButtonDown ("PauseButton")) {
			audio.clip = Pause;
			audio.Play ();
			isPaused = true;
			GameLogicScript.GhostOn = false;

		} else if (isPaused) {
				
			if (Input.anyKeyDown) {
					isPaused = false;
					GameLogicScript.GhostOn = true;
					audio.clip = Unpause;
					audio.Play ();
				}
				
				ControlsImage.enabled = true;

				if (Input.GetJoystickNames ().Length > 0) {
					ControlsImage.sprite = sVR;
				} else {
					ControlsImage.sprite = sKeyboard;
				}
				Time.timeScale = 0;

			} else {
				ControlsImage.enabled = false;
				Time.timeScale = 1;
			}
		}
}
