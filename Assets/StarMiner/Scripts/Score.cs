using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour {

	public Player player;
	public Text scoreText;

	// Update is called once per frame
	void Update () {
		scoreText.text = player.score.ToString();
	}
}
