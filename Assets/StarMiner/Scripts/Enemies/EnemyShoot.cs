using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

	public Transform firePoint;
	public GameObject player;
	public GameObject enemyLaser;

	public float distance;
	public float laserCooldown;
	public float bulletforce = 20f;

	void Start() {
		player = FindObjectOfType<Player>().gameObject;
		laserCooldown = 5;
	}

	// Update is called once per frame
	void Update() {

		laserCooldown -= Time.deltaTime;
		if (laserCooldown < 0) {
			laserCooldown = 0;
		}
	}

	public void shoot() {
        if (laserCooldown == 0) {
            Instantiate(enemyLaser, firePoint.position, firePoint.rotation);
            laserCooldown = 2;
            FindObjectOfType<AudioManager>().Play("EnemyShoot");
        }
    }
}
