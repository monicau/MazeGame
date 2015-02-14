using UnityEngine;
using System.Collections;

public class WelcomeMenu : MonoBehaviour {

	public void CloseWelcomeMenu() {
		//Deactivate the welcome panel
		gameObject.SetActive (false);
		//Hide cursor
		Screen.showCursor = false;
	}
}
