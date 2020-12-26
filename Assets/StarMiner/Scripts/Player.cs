using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	
	public float Xvelocity;
	public float Yvelocity;
	public float maxSpeed;
	public int score;
	public int health;
	public float fuel;
	public float fuelPenaltyCooldown;
	public bool godMode;

	public Rigidbody2D rb;
	public UI UI;
	public Transform exhaust;
	public GameObject effect;
	public GameObject laser;
	public GameObject flames;
	public GameObject ironText;
	public GameObject goldText;
	public GameObject diamondText;
	public GameObject crystalText;
	public GameObject unknownText;
	public GameObject fuelText;


	// Use this for initialization
	void Start () {
		godMode = false;
	}
	
	// Update is called once per frame
	void Update () {
		Xvelocity = rb.GetPointVelocity(GetComponent<Player>().transform.position).x;
		Yvelocity = rb.GetPointVelocity(GetComponent<Player>().transform.position).y;

		if (godMode) {
			fuel = 100;
			health = 100;
		}
		
		fuelPenaltyCooldown -= Time.deltaTime;

		if (fuelPenaltyCooldown < 0) {
			fuelPenaltyCooldown = 0;
		}

		if (fuel == 0 && fuelPenaltyCooldown == 0) {
			health -= 1;
			fuelPenaltyCooldown = 0.25f;
		}

		if(health <= 0) {
			Time.timeScale = 0;
			UI.gameOver.SetActive(true);
		}

		//Controls (Maybe move this all to other script?
		if (Time.timeScale == 1) {

			if (Input.GetKeyDown(KeyCode.W)) {
			FindObjectOfType<AudioManager>().Play("PlayerEngine");
			if(fuel > 0) {
				effect = Instantiate(flames, exhaust.position, Quaternion.Euler(0, 0, 90));
			}
			
		}
	
			if (Input.GetKey(KeyCode.W)) {
				
				fuel -= 0.02f;//1min 20secs of fuel at this rate
				if (fuel < 0) {
					fuel = 0;
					Destroy(effect);
					FindObjectOfType<AudioManager>().Stop("PlayerEngine");
				} else {
					rb.AddForce(transform.up * 200);
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

			if (Input.GetKeyDown(KeyCode.S))
		{
			FindObjectOfType<AudioManager>().Play("PlayerEngine");
		}

			if (Input.GetKeyUp(KeyCode.S))
		{
			FindObjectOfType<AudioManager>().Stop("PlayerEngine");
		}

			if (Input.GetKey(KeyCode.S)) {
			fuel -= 0.02f;//2mins 40secs of fuel at this rate

			if (fuel < 0) {
				fuel = 0;
				FindObjectOfType<AudioManager>().Stop("PlayerEngine");
			} else {
				rb.AddForce(transform.up * -200);
			}
			

			if (rb.velocity.magnitude > maxSpeed)
			{
				rb.velocity = rb.velocity.normalized * maxSpeed;
			}
		}

			if (Input.GetKey(KeyCode.D)) {

			transform.Rotate(0.0f, 0.0f, -1.75f, Space.Self);
		}
		}

		if (Input.GetKeyUp(KeyCode.W)) {
			FindObjectOfType<AudioManager>().Stop("PlayerEngine");
			Destroy(effect);
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {

			if(UI.pauseMenu.activeSelf) {
				Time.timeScale = 1;
				UI.pauseMenu.SetActive(false);
			} else {
				Time.timeScale = 0;
				UI.pauseMenu.SetActive(true);
			}
		}
	}

	public Vector3 getVelocity() {
		Vector3 velocity = new Vector3(Xvelocity, Yvelocity, 0);
		return velocity;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Asteroid") {
			health -= 1;
		}

		if(collision.collider.tag == "EnemyLaser") {
			health -= 5;
			FindObjectOfType<AudioManager>().Play("AsteroidHit");
		}
	}

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag == "Planet") {
			rb.drag = 1.5f;
		}
	}

    //Collision Detection Stuff (Maybe move all this to another script?)
    void OnTriggerEnter2D(Collider2D collision) {

		string tag = collision.tag;

		switch(tag) {
			case "Iron":
				score += 10;
				GameObject text = Instantiate(ironText, collision.transform.position + new Vector3(-0.35f, -0.25f, 0), Quaternion.identity);
				FindObjectOfType<AudioManager>().Play("Pickup");
				Destroy(collision.gameObject);
				Destroy(text, 0.75f);
				break;

			case "Gold":
				score += 20;
				text = Instantiate(goldText, collision.transform.position + new Vector3(-0.35f, -0.25f, 0), Quaternion.identity);
				FindObjectOfType<AudioManager>().Play("Pickup");
				Destroy(collision.gameObject);
				Destroy(text, 0.75f);
				break;

			case "Diamond":
				score += 50;
				text = Instantiate(diamondText, collision.transform.position + new Vector3(-0.35f, -0.25f, 0), Quaternion.identity);
				FindObjectOfType<AudioManager>().Play("Pickup");
				Destroy(collision.gameObject);
				Destroy(text, 0.75f);
				break;

			case "mysterousCrystal":
				score += 100;
				text = Instantiate(crystalText, collision.transform.position + new Vector3(-0.35f, -0.25f, 0), Quaternion.identity);
				FindObjectOfType<AudioManager>().Play("Pickup");
				Destroy(collision.gameObject);
				Destroy(text, 0.75f);
				break;

			case "Unknown":
				score += 400;
				text = Instantiate(unknownText, collision.transform.position + new Vector3(-0.35f, -0.25f, 0), Quaternion.identity);
				FindObjectOfType<AudioManager>().Play("Pickup");
				Destroy(collision.gameObject);
				Destroy(text, 0.75f);
				break;

			case "Fuel":
				fuel += 30;
				if (fuel > 100) {
					fuel = 100;
				}
				text = Instantiate(fuelText, collision.transform.position + new Vector3(-0.35f, -0.25f, 0), Quaternion.identity);
				FindObjectOfType<AudioManager>().Play("Fuelup");
				Destroy(collision.gameObject);
				Destroy(text, 0.75f);
				break;

			case "Planet":
				rb.drag = 15.0f;
			break;


		}

	}
}

