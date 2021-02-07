using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour {

	public GameObject ironText;
	public GameObject goldText;
	public GameObject diamondText;
	public GameObject crystalText;
	public GameObject unknownText;
	public GameObject cargoFullText;

	private PlayerInteractions player;

    private void Start() {
		player = FindObjectOfType<Player>().GetComponent<PlayerInteractions>();
	}

    void OnTriggerEnter2D(Collider2D collision) {

		if(collision.tag == "Player") {
			if (player.isCargoFull()) {
				GameObject text = Instantiate(cargoFullText, this.transform.position + new Vector3(-0.35f, -0.25f, 0), Quaternion.identity);
				Destroy(text, 0.75f);
			} else {
				switch (this.tag) {
					case "Iron":
						spawnFloatingText(ironText);
						player.cargo[0]++;
						break;

					case "Gold":
						spawnFloatingText(goldText);
						player.cargo[1]++;
						break;

					case "Diamond":
						spawnFloatingText(diamondText);
						player.cargo[2]++;
						break;

					case "mysterousCrystal":
						spawnFloatingText(crystalText);
						player.cargo[3]++;
						break;

					case "Unknown":
						spawnFloatingText(unknownText);
						player.cargo[4]++;
						break;
				}
			}
		}
	}

	private void spawnFloatingText(GameObject floatingText) {
			GameObject text = Instantiate(floatingText, this.transform.position + new Vector3(-0.35f, -0.25f, 0), Quaternion.identity);
			FindObjectOfType<AudioManager>().Play("Pickup");
			Destroy(this.gameObject);
			Destroy(text, 0.75f);
		}
	}
