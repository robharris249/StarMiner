using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	
	public GameObject asteroid1;
	public GameObject asteroid2;

	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject enemy4;
	public GameObject enemy5;

	public GameObject planetEarth;
	public GameObject planetRed;
	public GameObject planetOrange;
	public GameObject planetBlue;
	public GameObject planetPurple;

	public int enemyCount;
	public int asteroidCount;

	// Use this for initialization
	void Start () {

		GameObject planet;

		//Planet Earth 
		planet = Instantiate(planetEarth, new Vector3(-76.9f, 75.4f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "Aetis";

		planet = Instantiate(planetEarth, new Vector3(33.1f, 48.7f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "Etera";

		planet = Instantiate(planetEarth, new Vector3(-32.9f, -33.6f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "Yavin";

		//PlanetRed
		planet = Instantiate(planetRed, new Vector3(-63.3f, 36.4f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "Trion";

		planet = Instantiate(planetRed, new Vector3(-7.8f, -64.8f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "Char";

		planet = Instantiate(planetRed, new Vector3(83.2f, 40.8f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "Nixuvis";

		//Planet Orange
		planet = Instantiate(planetOrange, new Vector3(-81.1f, 65.3f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "Nezuno";

		planet = Instantiate(planetOrange, new Vector3(63.7f, -20.8f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "Sarunus";

		planet = Instantiate(planetOrange, new Vector3(-9.2f, 23.4f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "Bohibos";

		//Planet Blue
		planet = Instantiate(planetBlue, new Vector3(63.8f, 75.2f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "Lucao";

		planet = Instantiate(planetBlue, new Vector3(-70.2f, -13.3f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "Zuwei";

		planet = Instantiate(planetBlue, new Vector3(29.5f, -51.9f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "Laythe";

		//Planet Purple
		planet = Instantiate(planetPurple, new Vector3(77.6f, -68.3f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "Gethilea";

		planet = Instantiate(planetPurple, new Vector3(-5.5f, 64.1f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "LLevutania";

		planet = Instantiate(planetPurple, new Vector3(49.5f, 0.4f, 0.0f), Quaternion.identity);
		planet.GetComponent<Planet>().name = "Persephone";


		for (int i= -99; i < 100; i++) { //X co-ords
			for (int j = -99; j < 100; j++) {// Y co-cords
				bool x = (i > -5 && i < 5);
				bool y = (j > -5 && j < 5);

				if (x && y) {
					//Debug.Log("Don't put an asteroid at co-ords: (" + i + "," + j + ")");
					break;
				}

				int spawnChance = Random.Range(1, 50);
				if(spawnChance >= 45) {
					asteroidCount++;
					int asteroidType = Random.Range(1, 2);
					Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
					if (asteroidType == 1) {
						Instantiate(asteroid1, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), randomRotation);
					} else {
						Instantiate(asteroid2, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), randomRotation);
					}
				}
				if(spawnChance == 10) {
					int enemyType = Random.Range(1, 20);
					if(enemyType == 0 || enemyType > 5) {
						//do nothing, this is here to further reduce amount of enemy ships
					}
					else if (enemyType == 1) {
						Instantiate(enemy1, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), Quaternion.identity);
						enemyCount++;
					}
					else if(enemyType == 2) {
						Instantiate(enemy2, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), Quaternion.identity);
						enemyCount++;
					}
					else if(enemyType == 3) {
						Instantiate(enemy3, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), Quaternion.identity);
						enemyCount++;
					}
					else if(enemyType == 4) {
						Instantiate(enemy4, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), Quaternion.identity);
						enemyCount++;
					} else {
						Instantiate(enemy5, new Vector3(i + Random.Range(-0.25f, 0.25f), j + Random.Range(-0.25f, 0.25f), -1), Quaternion.identity);
						enemyCount++;
					}
				}
			}
		}
	}
}
