using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameButton : MonoBehaviour {

    public Scene nextLevel;
    public string nextLevelText;
	public SatelliteDish satelliteDish;
    private bool canQuit = false;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (canQuit)
        {
            SceneManager.LoadScene(nextLevelText);
        }
    }

	void OnCollisionEnter(Collision collision)
	{
		if (collision.rigidbody) {
			Debug.Log ("YOU WIN");
			satelliteDish.GetComponent<SatelliteDish> ().ButtonPress();
            StartCoroutine(Delay());
		}
	}

    IEnumerator Delay(){
        yield return new WaitForSeconds(2f);

        canQuit = true;
    }
}
