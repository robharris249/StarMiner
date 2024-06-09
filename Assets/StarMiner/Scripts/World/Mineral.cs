using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour {

	public GameObject mineralText;
	public GameObject cargoFullText;

	private PlayerInteractions player;
	private Vector3 offset = new Vector3(-0.35f, -0.255f, 0);

    private void Start() {
		player = FindObjectOfType<Player>().GetComponent<PlayerInteractions>();
	}

    void OnTriggerEnter2D(Collider2D collision) {


		if(collision.tag == "Player") {
			if (player.isCargoFull()) {
				GameObject text = Instantiate(cargoFullText, this.transform.position + offset, Quaternion.identity);
				Destroy(text, 0.75f);
			} else {

				spawnFloatingText(mineralText);

				switch (this.tag) {
					case "Iron":
						player.cargo[0]++;
						break;

					case "Gold":
						player.cargo[1]++;
						break;

					case "Diamond":
						player.cargo[2]++;
						break;

					case "mysterousCrystal":
						player.cargo[3]++;
						break;

					case "Unknown":
						player.cargo[4]++;
						break;
				}
			}
		}
	}

	private void spawnFloatingText(GameObject floatingText) {
			GameObject text = Instantiate(floatingText, this.transform.position + offset, Quaternion.identity);
			FindObjectOfType<AudioManager>().Play("Pickup");
			Destroy(this.gameObject);
			Destroy(text, 0.75f);
		}
	}
