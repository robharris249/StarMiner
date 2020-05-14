using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public float leftMenuTimer;
	public float rightMenuTimer;

	public GameObject controls;
	public GameObject enemies;
	public GameObject objectives;
	public GameObject resources;

	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject enemy4;
	public GameObject enemy5;

	void Start() {
		leftMenuTimer = 2.5f;
		rightMenuTimer = 5.0f;
	}

	// Update is called once per frame
	void Update() {
		leftMenuTimer -= Time.deltaTime;
		rightMenuTimer -= Time.deltaTime;

		if(leftMenuTimer <= 0) {
			if(controls.activeSelf) {
				enemies.SetActive(true);
				controls.SetActive(false);
			} else {
				controls.SetActive(true);
				enemies.SetActive(false);
			}
			leftMenuTimer = 5.0f;
		}

		if(rightMenuTimer <= 0) {
			if(objectives.activeSelf) {
				resources.SetActive(true);
				objectives.SetActive(false);
			} else {
				objectives.SetActive(true);
				resources.SetActive(false);
			}
			rightMenuTimer = 5.0f;
		}

		enemy1.transform.Rotate(0.0f, 0.0f, 1.0f, Space.Self);
		enemy2.transform.Rotate(0.0f, 0.0f, 1.0f, Space.Self);
		enemy3.transform.Rotate(0.0f, 0.0f, 1.0f, Space.Self);
		enemy4.transform.Rotate(0.0f, 0.0f, 1.0f, Space.Self);
		enemy5.transform.Rotate(0.0f, 0.0f, 1.0f, Space.Self);
	}

	public void playGame() {
		SceneManager.LoadScene("Level1");
		Time.timeScale = 1;
	}

	public void quit() {
		Application.Quit();
	}
}
