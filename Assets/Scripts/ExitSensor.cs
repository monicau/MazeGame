using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitSensor : MonoBehaviour {
	public GameObject fireworkPrefab;
	private GameObject firework;
	private GameObject firework2;
	private Text ammoDisplay;
	private Text lose;
	private Text lose2;
	private Text helper;

	void Start() {
		//Grab text objects
		GameObject ammoDisplayObj = GameObject.Find ("AmmoDisplay");
		ammoDisplay = ammoDisplayObj.GetComponent<Text> ();
		GameObject loseUIText = GameObject.Find ("Lose");
		lose = loseUIText.GetComponent<Text> ();
		GameObject lose2UIText = GameObject.Find ("Lose2");
		lose2 = lose2UIText.GetComponent<Text> ();
		GameObject helperText = GameObject.Find ("HelperText");
		helper = helperText.GetComponent<Text> ();
	}

	void OnTriggerStay() {
		//Enter here when player passes through exit
		//Display the win message
		ammoDisplay.text = "Power: happiness";
		lose.text="You WIN!";
		lose.color = Color.green;
		lose2.text="Congratulations";
		helper.text = "Press R to restart or X to exit";
		//Create fireworks 
		firework = Instantiate (fireworkPrefab) as GameObject;
		firework.transform.position = transform.position;
		Destroy (firework);
		firework2 = Instantiate (fireworkPrefab) as GameObject;
		firework2.transform.position = transform.position + new Vector3(3f, 0f, 0f);
	}
}
