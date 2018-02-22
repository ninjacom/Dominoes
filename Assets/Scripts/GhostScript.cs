using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {

	public bool placable;
	float xExtent;

	// Use this for initialization
	void Start () {
		placable = true;
		Renderer rend = GetComponent<Renderer> ();
		xExtent = rend.bounds.extents.x;
	}
	
	// Update is called once per frame
	void Update () {
		// if no dominoes left, no placable
		if (GameObject.Find ("GameController").GetComponent<GameLogic2> ().dominoCount <= 0) {
			placable = false;
		} else {

			//Raycast down. If hits surface, placable. If hits something else or nothing, not placable
			//make rays from the middle and both ends. all have to hit surface to be placeable
			Ray CentreDownRay = new Ray (transform.position, Vector3.down);
			Ray End1DownRay = new Ray (transform.position + new Vector3 (xExtent, 50f, 0f), Vector3.down);
			Ray End2DownRay = new Ray (transform.position + new Vector3 ( - xExtent, 50f, 0f), Vector3.down);
			RaycastHit hit, hitEnd1, hitEnd2;
			if (Physics.Raycast (CentreDownRay, out hit, 5f) && Physics.Raycast (End1DownRay, out hitEnd1, 70f) && Physics.Raycast (End2DownRay, out hitEnd2, 70f))
			{
				if (hit.collider.gameObject.CompareTag ("Surface") && hitEnd1.collider.gameObject.CompareTag ("Surface") && hitEnd2.collider.gameObject.CompareTag ("Surface"))
				{
					placable = true;

				} else {
					placable = false;
				}

			} else {
				placable = false;
			}

		}

		// If placable, Change colour to green. If not, red. 
		if (placable) {
			GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
		} else {
			GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
		}

	}
}
