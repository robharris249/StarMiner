using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	public float maxSpeed;
	public GameObject flames;
	GameObject exhaustEffect;
	public Rigidbody2D rb;
	public Transform exhaust;
	public UI UI;
	public bool docked;
	public GameObject shop;
	public GameObject radar;
	GameObject radarEffect;

    // Update is called once per frame
    void FixedUpdate() {
		
		if (Time.timeScale == 1) {

			if (Input.GetKey(KeyCode.W)) {

				if (GetComponent<Player>().fuel > 0) {
					rb.AddForce(transform.up * 135);
				}

				if (rb.velocity.magnitude > maxSpeed) {
					rb.velocity = rb.velocity.normalized * maxSpeed;
				}
			}

			if (Input.GetKey(KeyCode.A)) {
				transform.Rotate(0.0f, 0.0f, 1.75f, Space.Self);
			}

			if (Input.GetKey(KeyCode.S)) {
				GetComponent<Player>().fuel -= 0.02f;//2mins 40secs of fuel at this rate

				if (GetComponent<Player>().fuel > 0) {
					rb.AddForce(transform.up * -135);
				}

				if (rb.velocity.magnitude > maxSpeed) {
					rb.velocity = rb.velocity.normalized * maxSpeed;
				}
			}

			if (Input.GetKey(KeyCode.D)) {

				transform.Rotate(0.0f, 0.0f, -1.75f, Space.Self);
			}
		}
	}

    void Update() {

		if (Time.timeScale == 1) {

			if (Input.GetKeyDown(KeyCode.W)) {
				if (GetComponent<Player>().fuel > 0) {//if fuel is more than Zero
					FindObjectOfType<AudioManager>().Play("PlayerEngine");//Play engine sound
					exhaustEffect = Instantiate(flames, exhaust.position, Quaternion.Euler(0, 0, 90));//play flames animation
				}
			}

			if (Input.GetKey(KeyCode.W)) {

				if(!docked) {
					GetComponent<Player>().fuel -= Time.deltaTime;
				}
				
				if (GetComponent<Player>().fuel > 0) {
					exhaustEffect.transform.position = exhaust.position;
					exhaustEffect.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);
				} else {
					GetComponent<Player>().fuel = 0;
					FindObjectOfType<AudioManager>().Stop("PlayerEngine");
					Destroy(exhaustEffect);
				}
			}

			if (Input.GetKeyUp(KeyCode.W)) {
				FindObjectOfType<AudioManager>().Stop("PlayerEngine");
				Destroy(exhaustEffect);
			}

			if (Input.GetKeyDown(KeyCode.S)) {

				FindObjectOfType<AudioManager>().Play("PlayerEngine");
			}

			if (Input.GetKey(KeyCode.S)) {

				if (!docked) {
					GetComponent<Player>().fuel -= Time.deltaTime;
				}

				if (GetComponent<Player>().fuel < 0) {
					GetComponent<Player>().fuel = 0;
					FindObjectOfType<AudioManager>().Stop("PlayerEngine");
				}
			}

			if (Input.GetKeyUp(KeyCode.S)) {
				FindObjectOfType<AudioManager>().Stop("PlayerEngine");
			}

			if (docked && Input.GetKey(KeyCode.F)) {
				shop.SetActive(true);
				shop.GetComponent<Shop>().setUpShop();
				Time.timeScale = 0;
			}

			if(Input.GetKeyDown(KeyCode.R)) {

				
				if(!GetComponent<PlayerRadar>().searching) {
					FindObjectOfType<AudioManager>().Play("Radar");
					radarEffect = Instantiate(radar, transform.position, Quaternion.identity);
					Destroy(radarEffect, 2.8f);
					GetComponent<PlayerRadar>().GetThreeClosestPlanets();
				} else {
										
					if(radarEffect != null) {
						Destroy(radarEffect);
						FindObjectOfType<AudioManager>().Stop("Radar");
					}
					
					GetComponent<PlayerRadar>().planetArrows[0].SetActive(false);
					GetComponent<PlayerRadar>().planetArrows[1].SetActive(false);
					GetComponent<PlayerRadar>().planetArrows[2].SetActive(false);
					GetComponent<PlayerRadar>().closestPlanets[0].GetComponent<Planet>().arrow.SetActive(false);
					GetComponent<PlayerRadar>().closestPlanets[1].GetComponent<Planet>().arrow.SetActive(false);
					GetComponent<PlayerRadar>().closestPlanets[2].GetComponent<Planet>().arrow.SetActive(false);
				}
				GetComponent<PlayerRadar>().searching = !GetComponent<PlayerRadar>().searching;//flip state

				
			}

			
			if(radarEffect != null) {
				radarEffect.transform.position = transform.position;
            }
			
			
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
