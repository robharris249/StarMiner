using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Player player;
	public Text scoreText;
	public Text gameOverScore;
	public Text healthText;
	public Text fuelText;
	public Text frameRate;
	public Text enemyCount;
	public Text asteroidCount;
	public Text time;
	public GameObject gameOver;
	public GameObject pauseMenu;
	public GameObject toolsUI;
	public float frameRateCooldown = 1.0f;
	public int temptime;

	void Start() {
		enemyCount.text = FindObjectOfType<Spawner>().enemyCount.ToString();
		asteroidCount.text = FindObjectOfType<Spawner>().asteroidCount.ToString();
	}

	// Update is called once per frame
	void Update () {
		fuelText.text = player.fuel.ToString("F2");
		healthText.text = player.health.ToString();
		scoreText.text = player.score.ToString();
		gameOverScore.text = player.score.ToString();
		frameRateCooldown -= Time.deltaTime;
		if(frameRateCooldown < 0) {
			frameRate.text = (Time.frameCount / Time.time).ToString("F2") + " /s";
			frameRateCooldown = 1.0f;
		}

		temptime = (int)Time.time;
		if(temptime <= 60) {
			time.text = temptime.ToString("F0") + " Secs";
		} else {
			time.text = temptime / 60 + " Mins " + temptime % 60 + " Secs"; 
		}


	}

	public void playGame() {
		SceneManager.LoadScene("Level1");
		Time.timeScale = 1;
	}

	public void quit() {
		Application.Quit();
	}

	public void menu() {
		SceneManager.LoadScene("MainMenu");
	}

	public void resume() {
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
	}

	public void cheats() {
		if (FindObjectOfType<Player>().godMode) {
			FindObjectOfType<Player>().godMode = false;
			GameObject.Find("cheatsButton").GetComponentInChildren<Text>().text = "Disabled";
		} else {
			FindObjectOfType<Player>().godMode = true;
			GameObject.Find("cheatsButton").GetComponentInChildren<Text>().text = "Enabled";
		}
	}

	public void tools() {
		if (toolsUI.activeSelf) {
			toolsUI.SetActive(false);
			GameObject.Find("toolsButton").GetComponentInChildren<Text>().text = "Disabled";
		} else {
			toolsUI.SetActive(true);
			GameObject.Find("toolsButton").GetComponentInChildren<Text>().text = "Enabled";
		}
	}
}
