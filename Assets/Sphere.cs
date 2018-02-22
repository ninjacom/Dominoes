using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {

	private AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource> ();
	}
	
	void Update () {
		
	}

	void OnCollisionEnter(Collision collide) {
		if (collide.gameObject.tag == "Surface") {
			audio.Play ();
		}
	}
}
