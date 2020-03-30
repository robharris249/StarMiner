using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public float health = 100;
	public GameObject explosion;
	public GameObject Shattered;
	public GameObject Iron;
	public GameObject Gold;
	public GameObject Diamond;
	public GameObject Uranium;
	public GameObject Unknown;
	public GameObject Fuel;

	//resource chances
	public float ironChance;
	public float goldChance;
	public float diamondChance;
	public float uraniumChance;
	public float unknownChance;
	public float fuelChance;
	
	// Update is called once per frame
	void Update () {
		if(health <= 0) {
			Instantiate(Shattered, gameObject.transform.position, Quaternion.identity);

			GameObject effect = Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(effect, 0.75f);

			float chanceRoll = Random.RandomRange(0, 101);

			if (chanceRoll >= 0 && chanceRoll < 55) {
				//Asteroid is empty
			}
			else if (chanceRoll > 55 && chanceRoll < 70) {
				//Asteroid drops Iron
				Instantiate(Iron, gameObject.transform.position, Quaternion.identity);
			}
			else if (chanceRoll > 70 && chanceRoll < 82) {
				//Asteroid drops Gold
				Instantiate(Gold, gameObject.transform.position, Quaternion.identity);
			}
			else if (chanceRoll > 82 && chanceRoll < 92) {
				//Asteroid drops Diamond
				Instantiate(Diamond, gameObject.transform.position, Quaternion.identity);
			}
			else if (chanceRoll > 92 && chanceRoll < 97) {
				//Asteroid drops Uranium
				Instantiate(Uranium, gameObject.transform.position, Quaternion.identity);
			}
			else if (chanceRoll > 97 && chanceRoll < 99) {
				//Asteroid drops Unknown
				Instantiate(Unknown, gameObject.transform.position, Quaternion.identity);
			}
			else if (chanceRoll > 99) {
				//Asteroid drops Fuel
				Instantiate(Fuel, gameObject.transform.position, Quaternion.identity);
			}

			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.collider.tag == "SmallLaser")	{
			health -= 25;
			if (health > 0)
			{
				FindObjectOfType<AudioManager>().Play("AsteroidHit");
			}
			else
			{
				FindObjectOfType<AudioManager>().Play("AsteroidDestroy");

			}
		}
		/* //TODO BigLaser
		else if (collision.collider.tag == "BigLaser") {
			health -= 50;
			if (health > 0)
			{
				FindObjectOfType<AudioManager>().Play("AsteroidHit");
			}
			else
			{
				FindObjectOfType<AudioManager>().Play("AsteroidDestroy");

			}
		}
		*/
	}
}
