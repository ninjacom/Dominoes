using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RestartButton : MonoBehaviour {

	private AudioSource audio;
    // Use this for initialization
    void Start()
    {
		audio = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void ClickButton() {
		audio.Play ();
	}
}
