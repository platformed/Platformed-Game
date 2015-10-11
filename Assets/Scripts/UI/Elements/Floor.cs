using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Floor : MonoBehaviour {
	public Text text;

	void Update () {
		text.text = CameraMove.floor.ToString();
	}
}
