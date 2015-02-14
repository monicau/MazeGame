using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProjectileDestroyer : MonoBehaviour {
	public string target;
	public GameObject fireworkPrefab;
	public GameObject smokePrefab;
	private GameObject firework;
	private GameObject smoke;
	private Text lose;
	private Text lose2;
	private Text helper;

	void Start() {
		//Grab UI text objects
		GameObject loseUIText = GameObject.Find ("Lose");
		lose = loseUIText.GetComponent<Text> ();
		GameObject lose2UIText = GameObject.Find ("Lose2");
		lose2 = lose2UIText.GetComponent<Text> ();
		GameObject helperText = GameObject.Find ("HelperText");
		helper = helperText.GetComponent<Text> ();
	}

	void OnCollisionEnter(Collision collider) {
		if (collider.gameObject.name == target) {
			//Destroy target
			Destroy (collider.gameObject);
			//Create firework particle
			firework = Instantiate (fireworkPrefab) as GameObject;
			firework.transform.position = transform.position;
			//Destroy firework after 2 seconds
			Destroy (firework, 2);
			//Destroy self
			Destroy (gameObject);
		} else {
			smoke = Instantiate (smokePrefab) as GameObject;
			smoke.transform.position = transform.position;
			//Destroy smoke after 5 seconds
			Destroy(smoke, 5);
			//Destroy self
			Destroy (gameObject);
			lose.text = "You Lose.";
			lose2.text = "You are stuck in the maze forever.";
			helper.text = "Press R to restart or X to exit";
		}
	}
}
