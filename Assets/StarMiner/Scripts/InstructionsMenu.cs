using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InstructionsMenu : MonoBehaviour {

	public GameObject player;
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject enemy4;
	public GameObject enemy5;


	// Update is called once per frame
	void Update() {
		player.transform.Rotate(0.0f, 0.0f, 60.0f * Time.deltaTime, Space.Self);
		enemy1.transform.Rotate(0.0f, 0.0f, 60.0f * Time.deltaTime, Space.Self);
		enemy2.transform.Rotate(0.0f, 0.0f, 60.0f * Time.deltaTime, Space.Self);
		enemy3.transform.Rotate(0.0f, 0.0f, 60.0f * Time.deltaTime, Space.Self);
		enemy4.transform.Rotate(0.0f, 0.0f, 60.0f * Time.deltaTime, Space.Self);
		enemy5.transform.Rotate(0.0f, 0.0f, 60.0f * Time.deltaTime, Space.Self);
	}

	public void play() {
		SceneManager.LoadScene("Level1");
		Time.timeScale = 1;
	}

	public void back() {
		SceneManager.LoadScene("MainMenu");
    }
}
