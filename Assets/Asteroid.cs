using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public float health = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		health -= 25;
	}
}
