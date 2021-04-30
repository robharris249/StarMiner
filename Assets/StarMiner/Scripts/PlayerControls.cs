using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	public float maxSpeed;
	public GameObject flames;
	public GameObject effect;
	public Rigidbody2D rb;
	public Transform exhaust;
	public UI UI;
	public bool docked;
	public GameObject shop;

    private void Start() {
    }

    // Update is called once per frame
    void Update() {
		//Controls (Maybe move this all to other script?
		if (Time.timeScale == 1) {

			if (Input.GetKeyDown(KeyCode.W)) {
				FindObjectOfType<AudioManager>().Play("PlayerEngine");
				if (GetComponent<Player>().fuel > 0) {
					effect = Instantiate(flames, exhaust.position, Quaternion.Euler(0, 0, 90));
				}

			}

			if (Input.GetKey(KeyCode.W)) {

				GetComponent<Player>().fuel -= 0.02f;
				if (GetComponent<Player>().fuel < 0) {
					GetComponent<Player>().fuel = 0;
					Destroy(effect);
					FindObjectOfType<AudioManager>().Stop("PlayerEngine");
				} else {
					rb.AddForce(transform.up * 135);
					effect.transform.position = exhaust.transform.position;
					effect.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 90);
				}

				if (rb.velocity.magnitude > maxSpeed) {
					rb.velocity = rb.velocity.normalized * maxSpeed;
				}
			}

			if (Input.GetKey(KeyCode.A)) {
				transform.Rotate(0.0f, 0.0f, 1.75f, Space.Self);
			}

			if (Input.GetKeyDown(KeyCode.S)) {
				FindObjectOfType<AudioManager>().Play("PlayerEngine");
			}

			if (Input.GetKeyUp(KeyCode.S)) {
				FindObjectOfType<AudioManager>().Stop("PlayerEngine");
			}

			if (Input.GetKey(KeyCode.S)) {
				GetComponent<Player>().fuel -= 0.02f;//2mins 40secs of fuel at this rate

				if (GetComponent<Player>().fuel < 0) {
					GetComponent<Player>().fuel = 0;
					FindObjectOfType<AudioManager>().Stop("PlayerEngine");
				} else {
					rb.AddForce(transform.up * -135);
				}


				if (rb.velocity.magnitude > maxSpeed) {
					rb.velocity = rb.velocity.normalized * maxSpeed;
				}
			}

			if (Input.GetKey(KeyCode.D)) {

				transform.Rotate(0.0f, 0.0f, -1.75f, Space.Self);
			}


			if (docked && Input.GetKey(KeyCode.F)) {
				shop.SetActive(true);
				shop.GetComponent<Shop>().setUpShop();
				Time.timeScale = 0;
			}
		}

		if (Input.GetKeyUp(KeyCode.W)) {
			FindObjectOfType<AudioManager>().Stop("PlayerEngine");
			Destroy(effect);
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {

			if (UI.pauseMenu.activeSelf) {
				Time.timeScale = 1;
				UI.pauseMenu.SetActive(false);
			} else {
				Time.timeScale = 0;
				UI.pauseMenu.SetActive(true);
			}
		}
	}
}
