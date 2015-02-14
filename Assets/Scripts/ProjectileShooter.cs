using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProjectileShooter : MonoBehaviour {
	public GameObject projectilePrefab1;
	public GameObject projectilePrefab2;
	public GameObject projectilePrefab3;
	public GameObject ammunitionPrefab1;
	public GameObject ammunitionPrefab2;
	public GameObject ammunitionPrefab3;
	private Text ammoDisplay;
	private Text helperText;
	
	void Start() {
		//Set our player to hold no ammunition
		GameVariables.key = 0;
		//Grab UI text objects
		GameObject ammoDisplayObj = GameObject.Find ("AmmoDisplay");
		ammoDisplay = ammoDisplayObj.GetComponent<Text> ();
		GameObject helperObj = GameObject.Find ("HelperText");
		helperText = helperObj.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		//Detect if player shoots projectile
		if (Input.GetMouseButtonDown (0) && GameVariables.key != 0) {
			GameObject projectile = null;
			//Shoot the right projectile
			if (GameVariables.key == 1) {
				projectile = Instantiate (projectilePrefab1) as GameObject;
			} else if (GameVariables.key == 2) {
				projectile = Instantiate (projectilePrefab2) as GameObject;
			} else if (GameVariables.key==3) {
				projectile = Instantiate (projectilePrefab3) as GameObject;
			}
			//Make the projectile shoot forward
			projectile.transform.position = transform.position + Camera.main.transform.forward * 0.5f;
			Rigidbody rb = projectile.GetComponent<Rigidbody>();
			rb.velocity = Camera.main.transform.forward * 10;
			//Set player's current ammunition to be empty
			GameVariables.key = 0;
			ammoDisplay.text = "Power: none";
			ammoDisplay.color = Color.grey;
			helperText.text = "";
		}
		//Detect if player drops projectile
		if (Input.GetKey(",") && GameVariables.key != 0) {
			GameObject ammunition = null;
			//Drop the correct ammunition
			if (GameVariables.key == 1) {
				ammunition = Instantiate (ammunitionPrefab1) as GameObject;
			} else if (GameVariables.key == 2 ) {
				ammunition = Instantiate (ammunitionPrefab2) as GameObject;
			} else {
				ammunition = Instantiate (ammunitionPrefab3) as GameObject;
			}
			ammunition.transform.position = transform.position;
			//Set player's current ammunition to be empty
			GameVariables.key = 0;
			ammoDisplay.text = "Power: none";
			ammoDisplay.color = Color.grey;
			helperText.text = "";
		}
	}
}
