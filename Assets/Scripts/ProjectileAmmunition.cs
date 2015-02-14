using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProjectileAmmunition : MonoBehaviour {
	public int key;
	private Text ammoDisplay;
	private Text helperText;

	void Start() {
		//Grab UI Texts
		GameObject ammoDisplayObj = GameObject.Find ("AmmoDisplay");
		ammoDisplay = ammoDisplayObj.GetComponent<Text> ();
		GameObject helperObj = GameObject.Find ("HelperText");
		helperText = helperObj.GetComponent<Text> ();
	}
	void OnTriggerStay() {
		//When player is within range of ammunition, show helper text
		helperText.text = "Press \".\" to pick up";
		//Detect key press "." for picking up ammunition
		if (Input.GetKey (".")) {
			GameVariables.key = key;
			Destroy(gameObject);
			//Change our UI text depending on which ammunition was picked up
			if (key == 1) {
				ammoDisplay.text = "Power: RED";
				ammoDisplay.color = Color.red;
			} else if (key == 2) {
				ammoDisplay.text = "Power: GREEN";
				ammoDisplay.color = Color.green;
			} else {
				ammoDisplay.text = "Power: BLUE";
				ammoDisplay.color = Color.blue;
			}
			//Show helper text of how to use ammunition
			helperText.text = "Left mouse-click to shoot or press \",\" to drop projectile. NOTE: You only have one shot.";
		}
	}
	void OnTriggerExit() {
		//Remove helper text when player exits area of ammunition
		helperText.text = "";
		
	}
	
}
