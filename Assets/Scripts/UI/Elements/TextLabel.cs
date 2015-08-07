using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextLabel : MonoBehaviour {
	public Text obj;
	string text;

	void Start () {
	
	}

	void Update () {
		obj.text = text;
	}

	public void setText(string t){
		text = t;
	}
}
