using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour {

	public Player player;
	public Text healthText;

	// Update is called once per frame
	void Update()
	{
		healthText.text = player.health.ToString();
	}
}
