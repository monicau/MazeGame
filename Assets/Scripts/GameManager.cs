using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;
	private Maze mazeInstance;

	// Use this for initialization
	void Start () {
		StartGame ();	

		GameObject loseUIText = GameObject.Find ("Lose");
		Text lose = loseUIText.GetComponent<Text> ();
		GameObject lose2UIText = GameObject.Find ("Lose2");
		Text lose2 = lose2UIText.GetComponent<Text> ();
		lose.text = "";
		lose2.text = "";
	}
	void Update() {
		//Restart game when user presses R
		if (Input.GetKeyDown (KeyCode.R)) {
			//Restart();
			Screen.showCursor = true;
			Application.LoadLevel("MazeGame");
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			Application.Quit();
		}
	}
	void StartGame() {
		mazeInstance = Instantiate (mazePrefab) as Maze;
		mazeInstance.GenerateMaze ();
	}
	void Restart() {
		Destroy (mazeInstance.gameObject);
		StartGame ();
	}
}
