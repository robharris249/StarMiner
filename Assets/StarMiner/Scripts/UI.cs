using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Player player;
	public Text creditsTextLabel;
	public Text creditsText;
	public Text healthTextLabel;
	public Text healthText;
	public Text fuelTextLabel;
	public Text fuelText;
	public Text frameRate;
	public Text enemyCount;
	public Text asteroidCount;
	public Text time;
	public Text CoordsLabel;
	public Text xCoords;
	public Text yCoords;
	public Text gameOverScore;
	public GameObject gameOver;
	public GameObject pauseMenu;
	public GameObject toolsUI;
    public GameObject Radar;
    public float frameRateCooldown = 1.0f;
	public int temptime;
	public Shop Shop;

	public void DockToggle() {
		creditsTextLabel.gameObject.SetActive(!creditsTextLabel.gameObject.activeSelf);
		creditsText.gameObject.SetActive(!creditsText.gameObject.activeSelf);
		healthTextLabel.gameObject.SetActive(!healthTextLabel.gameObject.activeSelf);
		healthText.gameObject.SetActive(!healthText.gameObject.activeSelf);
		fuelTextLabel.gameObject.SetActive(!fuelTextLabel.gameObject.activeSelf);
		fuelText.gameObject.SetActive(!fuelText.gameObject.activeSelf);
		CoordsLabel.gameObject.SetActive(!CoordsLabel.gameObject.activeSelf);
		xCoords.gameObject.SetActive(!xCoords.gameObject.activeSelf);
		yCoords.gameObject.SetActive(!yCoords.gameObject.activeSelf);
		player.GetComponent<PlayerRadar>().planetArrows[0].gameObject.SetActive(false);
		player.GetComponent<PlayerRadar>().planetArrows[1].gameObject.SetActive(false);
		player.GetComponent<PlayerRadar>().planetArrows[2].gameObject.SetActive(false);
        player.GetComponent<PlayerRadar>().closestPlanets[0].GetComponent<Planet>().arrow.SetActive(false);
        player.GetComponent<PlayerRadar>().closestPlanets[1].GetComponent<Planet>().arrow.SetActive(false);
        player.GetComponent<PlayerRadar>().closestPlanets[2].GetComponent<Planet>().arrow.SetActive(false);
		player.GetComponent<PlayerRadar>().searching = false;
		Shop.planet.txtName.gameObject.SetActive(!Shop.planet.txtName.gameObject.activeSelf);
		Shop.planet.instructions.gameObject.SetActive(!Shop.planet.instructions.gameObject.activeSelf);
    }

    void Start() {
		enemyCount.text = FindObjectOfType<Spawner>().enemyCount.ToString();
		asteroidCount.text = FindObjectOfType<Spawner>().asteroidCount.ToString();
	}

	// Update is called once per frame
	void Update () {
		xCoords.text = player.transform.position.x.ToString("F1");
		yCoords.text = player.transform.position.y.ToString("F1");
		fuelText.text = player.fuel.ToString("F2");
		healthText.text = player.health.ToString();
		creditsText.text = player.credits.ToString("F2");
		gameOverScore.text = player.credits.ToString();
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
		Time.timeScale = 1;
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
