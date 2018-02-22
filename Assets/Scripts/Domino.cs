using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domino : MonoBehaviour {

	AudioSource audio;

    // Use this for initialization
    void Start () {
		audio = GetComponent<AudioSource> ();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.rigidbody) {
			audio.Play();
		}
	}
}
