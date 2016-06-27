using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloorText : MonoBehaviour {
	public Text text;

	void Update () {
		text.text = (CameraMove.floor - 127).ToString();
	}
}
