using UnityEngine.UI;
using UnityEngine;

public class Fuel : MonoBehaviour {

	public Player player;
	public Text fuelText;

	// Update is called once per frame
	void Update() {
		fuelText.text = player.fuel.ToString("F2");
	}
}
