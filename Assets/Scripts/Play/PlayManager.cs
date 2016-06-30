using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayManager : MonoBehaviour {
	public static PlayManager instance;
	public int Score { get; set; }

	public Text scoreText;
	
	void Awake () {
		instance = this;
	}
	
	void Update () {
		scoreText.text = Score.ToString();
	}
}
