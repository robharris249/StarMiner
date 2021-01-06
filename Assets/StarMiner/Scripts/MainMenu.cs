using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public void playGame() {
		SceneManager.LoadScene("Instructions");
	}

	public void quit() {
		Application.Quit();
	}
}
