using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	Image StandardDominoInventory;
	public Sprite s0,s1,s2,s3,s4,s5,s6,s7,s8,s9,s10,s11,s12;

	// Use this for initialization
	void Start () {
		StandardDominoInventory = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		int StandardDominoNum = GameObject.Find("GameController").GetComponent<GameLogic2>().dominoCount;
		switch (StandardDominoNum)
		{
		case 0:
			StandardDominoInventory.sprite = s0;
			break;
		case 1:
			StandardDominoInventory.sprite = s1;
			break;
		case 2:
			StandardDominoInventory.sprite = s2;
			break;
		case 3:
			StandardDominoInventory.sprite = s3;
			break;
		case 4:
			StandardDominoInventory.sprite = s4;
			break;
		case 5:
			StandardDominoInventory.sprite = s5;
			break;
		case 6:
			StandardDominoInventory.sprite = s6;
			break;
		case 7:
			StandardDominoInventory.sprite = s7;
			break;
		case 8:
			StandardDominoInventory.sprite = s8;
			break;
		case 9:
			StandardDominoInventory.sprite = s9;
			break;
		case 10:
			StandardDominoInventory.sprite = s10;
			break;
		case 11:
			StandardDominoInventory.sprite = s11;
			break;
		case 12:
			StandardDominoInventory.sprite = s12;
			break;


		}
	}

}
