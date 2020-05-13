using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public void playGame() {
		SceneManager.LoadScene("Level1");
		Time.timeScale = 1;
	}

	public void quit() {
		Application.Quit();
	}
}
