using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject laser;
	public float laserCooldown;
	public float Xvelocity;
	public float Yvelocity;
	public float maxSpeed = 20.0f;
	public Rigidbody2D rb;
	public int score;
	public int health;
	public float fuel;
	public GameObject flames;
	public Transform exhaust;
	public GameObject effect;
	public float fuelPenaltyCooldown;
	public GameObject gameOver;


	// Use this for initialization
	void Start () {
		laserCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Xvelocity = rb.GetPointVelocity(GetComponent<Player>().transform.position).x;
		Yvelocity = rb.GetPointVelocity(GetComponent<Player>().transform.position).y;

		laserCooldown -= Time.deltaTime;
		fuelPenaltyCooldown -= Time.deltaTime;

		if (laserCooldown < 0) {
			laserCooldown = 0;
        }

		if (fuelPenaltyCooldown < 0)
		{
			fuelPenaltyCooldown = 0;
		}

		if (fuel == 0 && fuelPenaltyCooldown == 0) {
			health -= 1;
			fuelPenaltyCooldown = 0.25f;
		}

		if(health <= 0) {
			Time.timeScale = 0;
			gameOver.SetActive(true);
		}

		//Controls (Maybe move this all to other script?
		if (Input.GetKeyDown(KeyCode.W)) {
			FindObjectOfType<AudioManager>().Play("PlayerEngine");
			if(fuel > 0) {
				effect = Instantiate(flames, exhaust.position, Quaternion.Euler(0, 0, 90));
			}
			
		}

		if (Input.GetKeyUp(KeyCode.W))
		{
			FindObjectOfType<AudioManager>().Stop("PlayerEngine");
			Destroy(effect);
		}

		if (Input.GetKey(KeyCode.W)) {
			fuel -= 0.01f;//2mins 40secs of fuel at this rate
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
			fuel -= 0.01f;//2mins 40secs of fuel at this rate

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

	public Vector3 getVelocity() {

		Vector3 velocity = new Vector3(Xvelocity, Yvelocity, 0);

		return velocity;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Asteroid") {
			health -= 10;
		}
	}

		//Collision Detection Stuff (Maybe move all this to another script?)
		void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Iron") {
			score += 10;
			Destroy(collision.gameObject);
		}

		else if (collision.tag == "Gold") {
			score += 20;
			Destroy(collision.gameObject);
		}

		else if (collision.tag == "Diamond") {
			score += 50;
			Destroy(collision.gameObject);
		}

		else if (collision.tag == "Uranium") {
			score += 100;
			Destroy(collision.gameObject);
		}

		else if (collision.tag == "Unknown") {
			score += 400;
			Destroy(collision.gameObject);
		}

		else if (collision.tag == "Fuel") {
			fuel += 30;
			if(fuel > 100) {
				fuel = 100;
			}
			Destroy(collision.gameObject);
		}
	}

}

