using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;


public class GameLogic : MonoBehaviour
{
	public GameObject DominoPrefab;
	public StartingDomino[] startingDomino = new StartingDomino[10];
	public StartButton startButton;
	public RestartButton restartButton;
    public int dominoCount;
	GameObject[] Selectables;
	string mode;
	int modeNum;
	List<string> modeList = new List<string>();
	Transform camera;
	public GameObject GhostPrefab;
	Transform Ghost;
	private AudioSource audio;
	bool canRestart = false;

	// Use this for initialization
	void Start()
	{
		audio = GetComponent<AudioSource> ();
		modeList.Add("Selector");
		modeList.Add("StandardDomino");
		modeNum = 0;
		mode = modeList[modeNum];
	}

	void Update()
	{
		camera = Camera.main.transform;

		// Changing Mode
		if (Input.GetButtonDown ("ModeIncrease") || Input.GetButtonDown ("ModeDecrease")) 
		{
			if (Input.GetButtonDown("ModeIncrease"))
            {
				modeNum = mod(modeNum + 1 , modeList.Count);
			} else if (Input.GetButtonDown("ModeDecrease")) {
				modeNum = mod(modeNum + 1 , modeList.Count);
			}

			mode = modeList [modeNum];

			if (mode == "StandardDomino") {
				InitialGhostPlacement();
			} else {
				Destroy (Ghost.gameObject);
			}
		}
			
		if (canRestart) {
			canRestart = false;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		//--------------Handling Objects in Line of Sight---------------------
		if (mode == "Selector") {


			Selectables = GameObject.FindGameObjectsWithTag("Selectable");
			foreach (GameObject Selectable in Selectables) {
				Selectable.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
			}


			Ray ray;
			RaycastHit[] hits;
			GameObject hitObject;
			ray = new Ray(camera.position, camera.rotation * Vector3.forward);
			hits = Physics.RaycastAll(ray);


			//detecting raycast hits
			for (int i = 0; i < hits.Length; i++)
			{
				RaycastHit hit = hits[i];
				hitObject = hit.collider.gameObject;
				if (Physics.Raycast(ray, out hit))
				{

					//if object in sightline has selectable tag, highlight it
					if (hitObject.CompareTag ("Selectable")) {
						hitObject.GetComponent<Renderer> ().material.shader = Shader.Find ("OutlinedSilhouetted");
					}

					if (Input.GetButtonDown("Fire1"))
					{
                        //click start button to knock over starting domino
                        if (hitObject.name.ToString() == startButton.name.ToString())
						{
                            for( i = 0; i < startingDomino.Length; i++)
                            {
                                startingDomino[i].FallDownByButtonPush();
                            }
							startButton.GetComponent<StartButton> ().ClickButton ();
						}

                        
                        if (hitObject.name.ToString() == restartButton.name.ToString())
                        {
                            Debug.Log("Restart");
							restartButton.GetComponent<RestartButton> ().ClickButton ();
							StartCoroutine(Delay());
                
						}

                        //remove currently placed dominos
                        if (hitObject.name.Contains ("Domino")) {
							Destroy (hitObject);
							dominoCount += 1;
						}

					}


					}

				}

		}
		//-----------------------------------------------

		if (mode == "StandardDomino")
		{
			float movementSpeed = 0.1f;
			float tiltAngle = 30f;
			float smooth = 2f;

			//Get inputs
			float LeftRightShift = Input.GetAxis ("LeftStickX");
			float ForwardBackShift = Input.GetAxis ("LeftStickY");
			float YRotation = Input.GetAxis ("RightStickX");
			Ghost.position += movementSpeed * LeftRightShift * camera.right;
			Ghost.position += movementSpeed * ForwardBackShift * camera.forward;

			Quaternion target = Quaternion.Euler (Ghost.rotation.eulerAngles + new Vector3 (0f, YRotation * tiltAngle, 0f));
			Ghost.rotation = Quaternion.Slerp (Ghost.rotation, target, Time.deltaTime * smooth);


			Ray DownRay = new Ray (new Vector3 (Ghost.position.x, 50f, Ghost.position.z), Vector3.down);
			RaycastHit hit;
			if (Physics.Raycast (DownRay, out hit, 51f)) {
				Ghost.position = new Vector3 (Ghost.position.x, hit.point.y + 0.2f, Ghost.position.z);
			} else {
				Ghost.GetComponent<GhostScript> ().placable = false;
			}

			//Place domino, place next ghost and destory current ghost.
			if (Input.GetButtonDown ("Fire1") && Ghost.GetComponent<GhostScript>().placable) {
				dominoCount -= 1;
				Instantiate (DominoPrefab, Ghost.position, Ghost.rotation);
				SubsequentGhostPlacement ();

			}

		}

	}


	void InitialGhostPlacement()
	{
		Vector3 InFrontOfCamera = camera.position + camera.rotation * Vector3.forward * 2.5f;
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
		audio.Play ();

	}

	IEnumerator Delay(){
		yield return new WaitForSeconds(0.5f);

		canRestart = true;
	}

	int mod(int x, int m) {
		return (x % m + m) % m;
	}
    
}