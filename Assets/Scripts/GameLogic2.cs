using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;


public class GameLogic2 : MonoBehaviour
{
	public bool GhostOn;
	public GameObject DominoPrefab;
	public StartingDomino[] startingDomino = new StartingDomino[10];
	public StartButton startButton;
	public RestartButton restartButton;
	public int dominoCount;
	GameObject[] Selectables;
	Transform PlayerCamera;
	public GameObject GhostPrefab;
	Transform Ghost;
	private AudioSource newDominoAudio;
	bool canRestart = false;

	// Use this for initialization
	void Start()
	{
		PlayerCamera = Camera.main.transform;
		newDominoAudio = GetComponent<AudioSource> ();
		if (GhostOn) {
			InitialGhostPlacement ();
		}
	}

	void Update()
	{
		PlayerCamera = Camera.main.transform;


		if (canRestart) {
			canRestart = false;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		//--------------Handling Objects in Line of Sight---------------------
		Selectables = GameObject.FindGameObjectsWithTag("Selectable");
		foreach (GameObject Selectable in Selectables) {
			Selectable.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
		}


		Ray ray;
		RaycastHit hit;
		ray = new Ray(PlayerCamera.position, PlayerCamera.rotation * Vector3.forward);

		//detecting raycast hits
			
			if (Physics.Raycast(ray, out hit))
			{
				GameObject hitObject = hit.collider.gameObject;

				//if object in sightline has selectable tag, highlight it
				if (hitObject.CompareTag ("Selectable")) {
					hitObject.GetComponent<Renderer> ().material.shader = Shader.Find ("OutlinedSilhouetted");
				}

			if (Input.GetButtonDown ("Fire1")) {
				//click start button to knock over starting domino
				if (hitObject.name.ToString () == startButton.name.ToString ()) {
					for (int i = 0; i < startingDomino.Length; i++) {
						startingDomino [i].FallDownByButtonPush ();
					}
					startButton.GetComponent<StartButton> ().ClickButton ();
				}

				//click reset button to reset scene
				if (hitObject.name.ToString () == restartButton.name.ToString ()) {
					Debug.Log ("Restart");
					restartButton.GetComponent<RestartButton> ().ClickButton ();
					StartCoroutine (Delay ());

				}
					

			} else if (Input.GetButtonDown ("DestroyDomino")) {
				//remove currently placed dominos
				if (hitObject.name.Contains ("Domino")) {
					Destroy (hitObject);
					dominoCount += 1;
				}

			}


			}


		//-----------------------------------------------
		if (GhostOn) {
			float movementSpeed = 0.1f;
			float tiltAngle = 30f;
			float smooth = 2f;

			//Get inputs
			float LeftRightShift = Input.GetAxis ("LeftStickX");
			float ForwardBackShift = Input.GetAxis ("LeftStickY");
			float YRotation = Input.GetAxis ("RightStickX");
			//move ghost left/right and towards/further away from the camera
			Ghost.position += movementSpeed * LeftRightShift * PlayerCamera.right;
			Ghost.position += movementSpeed * ForwardBackShift * PlayerCamera.forward;

			//rotate ghost
			Quaternion target = Quaternion.Euler (Ghost.rotation.eulerAngles + new Vector3 (0f, YRotation * tiltAngle, 0f));
			Ghost.rotation = Quaternion.Slerp (Ghost.rotation, target, Time.deltaTime * smooth);

			// places ghost on highest thing in its horizontal position
			Ray DownRay = new Ray (new Vector3 (Ghost.position.x, 50f, Ghost.position.z), Vector3.down);
			RaycastHit hitDown;
			if (Physics.Raycast (DownRay, out hitDown, 51f)) {
				Ghost.position = new Vector3 (Ghost.position.x, hitDown.point.y + 0.2f, Ghost.position.z);
			} else {
				Ghost.GetComponent<GhostScript> ().placable = false;
			}

			//Place domino, place next ghost and destory current ghost.
			if (Input.GetButtonDown ("Fire1") && Ghost.GetComponent<GhostScript> ().placable) {
				dominoCount -= 1;
				Instantiate (DominoPrefab, Ghost.position, Ghost.rotation);
				SubsequentGhostPlacement ();

			}
		}
	}




	public void InitialGhostPlacement()
	{
		Vector3 InFrontOfCamera = PlayerCamera.position + PlayerCamera.rotation * Vector3.forward * 2.5f;
		Vector3 InFrontOfCameraAndHighUp = new Vector3 (InFrontOfCamera.x, 50f, InFrontOfCamera.z);
		Ray DownRay = new Ray (InFrontOfCameraAndHighUp, Vector3.down);
		RaycastHit hit;
		if (Physics.Raycast (DownRay, out hit, 51f)) {
			Vector3 InitialGhostPos = hit.point + new Vector3 (0f, 0.2f, 0f);
			Ghost = Instantiate (GhostPrefab.transform, InitialGhostPos, Quaternion.identity);
		} else {
			Vector3 InitialGhostPos = InFrontOfCamera + new Vector3 (0f, 0.2f, 0f);
			Ghost = Instantiate (GhostPrefab.transform, InitialGhostPos, Quaternion.identity);
		}

	}

	void SubsequentGhostPlacement () 
	{
		float step = 0.7f;
		Vector3 newGhostPosition = Ghost.position + Ghost.forward * step;
		Quaternion newGhostRotation = Ghost.rotation;
		Destroy(Ghost.gameObject);
		Ghost = Instantiate(GhostPrefab.transform, newGhostPosition, newGhostRotation);
		newDominoAudio.Play ();

	}

	IEnumerator Delay(){
		yield return new WaitForSeconds(0.5f);

		canRestart = true;
	}

	int mod(int x, int m) {
		return (x % m + m) % m;
	}

}