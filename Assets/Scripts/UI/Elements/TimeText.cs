using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeText : MonoBehaviour {
	Text text;

	void Start() {
		text = GetComponent<Text>();
	}

	void Update() {
		text.text = UIManager.getTime();
	}
}
