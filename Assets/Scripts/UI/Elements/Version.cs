using UnityEngine;
using UnityEngine.UI;

public class Version : MonoBehaviour {
	void Start () {
		Text text = GetComponent<Text>();
		text.text = "Alpha v0.0.0";
	}
}
