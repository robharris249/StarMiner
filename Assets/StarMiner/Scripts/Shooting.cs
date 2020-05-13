using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public Transform firePoint;
	public GameObject smallLaserPrefab;
	public float laserCooldown;
	public float bulletforce = 20f;

	// Use this for initialization
	void Start() {
		laserCooldown = 0;
	}

	// Update is called once per frame
	void Update () {

		if (Time.timeScale == 1) {

			laserCooldown -= Time.deltaTime;
			if (laserCooldown < 0) {
				laserCooldown = 0;
			}

			if (Input.GetKey(KeyCode.Space) && laserCooldown == 0) {
				shoot();
				if(FindObjectOfType<Player>().godMode) {
					laserCooldown = 0.05f;
				} else {
					laserCooldown = 0.5f;
				}
				FindObjectOfType<AudioManager>().Play("PlayerShootSmall");
			}
		}
	}

	void shoot() {
		Instantiate(smallLaserPrefab, firePoint.position, firePoint.rotation);
    }
}
