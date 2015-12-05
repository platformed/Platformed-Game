using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelButton : MonoBehaviour {
	public Text levelName;
	public Text date;

	public void setName(string n) {
		levelName.text = n;
	}

	public void setDate(string d) {
		date.text = d;
	}

	void Start () {
	
	}

	void Update () {
	
	}
}
