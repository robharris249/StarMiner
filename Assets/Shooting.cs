using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public Transform firePoint;
	public GameObject smallLaserPrefab;
	public float laserCooldown;
	public float bulletforce = 20f;

	// Use this for initialization
	void Start()
	{
		laserCooldown = 0;
	}

	// Update is called once per frame
	void Update () {

		laserCooldown -= Time.deltaTime;
		if (laserCooldown < 0)
		{
			laserCooldown = 0;
		}

		if (Input.GetKey(KeyCode.Space) && laserCooldown == 0) {
			shoot();
			laserCooldown = 0.5f;
		}
	}

	void shoot()
    {
		GameObject laser = Instantiate(smallLaserPrefab, firePoint.position, firePoint.rotation);
		Rigidbody2D rb = smallLaserPrefab.GetComponent<Rigidbody2D>();
		//rb.AddForce(firePoint.up * bulletforce, ForceMode2D.Impulse);
    }
}
