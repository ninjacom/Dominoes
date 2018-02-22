using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAndPauseScreen : MonoBehaviour {

	Image ControlsImage;
	public Sprite sVR, sKeyboard, sTitle;
	bool isPaused, firstUnpause;
	public AudioClip Pause;
	public AudioClip Unpause;
	AudioSource audio;
	bool TitleScreenOn;
	GameLogic2 GameLogicScript;

	void Awake () {

		GameLogicScript = GameObject.Find ("GameController").GetComponent<GameLogic2> ();
		GameLogicScript.GhostOn = false;

	}


	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		isPaused = false;
		firstUnpause = true;
		TitleScreenOn = true;
		ControlsImage = GetComponent<Image> ();
		ControlsImage.sprite = sTitle;
		audio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		if (TitleScreenOn) {
			if (Input.anyKeyDown) {

				audio.clip = Unpause;
				audio.Play ();

				TitleScreenOn = false;
				isPaused = true;
			}
		} else {

			if (!isPaused && Input.GetButtonDown ("PauseButton")) {
				
				audio.clip = Pause;
				audio.Play ();

				isPaused = true;
				GameLogicScript.GhostOn = false;

			} else if (isPaused) {

				if (Input.anyKeyDown) {
					
					audio.clip = Unpause;
					audio.Play ();

					isPaused = false;
					GameLogicScript.GhostOn = true;

					if (firstUnpause) {
						GameLogicScript.InitialGhostPlacement ();
						firstUnpause = false;
					}
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
}
