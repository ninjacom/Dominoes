using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	Camera mycam;
	float step;

	// Use this for initialization
	void Start () {
		mycam = GetComponent<Camera>();
		step = 0.05f;
	}
	
	// Update is called once per frame
	void Update () {

		// If right mouse button is being held, move camera view towards cursor
		if (Input.GetMouseButton (1)) {

			Vector3 CursorPosition = mycam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, mycam.nearClipPlane));

			Vector3 newDir = Vector3.RotateTowards(transform.forward, CursorPosition - transform.position, step, 0.0F);
			transform.rotation = Quaternion.LookRotation(newDir);
		}
			
	}
}
