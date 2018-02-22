using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour {
    
	private AudioSource audio;
	private Animation anim;

    // Use this for initialization
    void Start () {
		audio = GetComponent<AudioSource> ();
		anim = GetComponent<Animation> ();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

	public void ClickButton() {
		audio.Play ();
		//anim.Play ("ButtonPress_Restart");
    }
}
