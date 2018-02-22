using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(translation: transform.forward * Mathf.Cos(Time.time) * Time.deltaTime * speed);
    }
}
