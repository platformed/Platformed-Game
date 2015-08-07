using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlatButton : MonoBehaviour {
	public Button button;
	public Image ripple;
	
	bool isPressed = false;
	float rippleSpeed = 8;
	float rippleFadeSpeed = 4;
	
	void Start () {
		ripple.rectTransform.localScale = new Vector3(0, 0, 1);
	}
	
	void Update () {
		ripple.rectTransform.localScale = Vector3.Lerp (ripple.rectTransform.localScale, new Vector3 (1, 1, 1), Time.deltaTime * rippleSpeed);
		if(!isPressed) {
			ripple.color = new Color(1f, 1f, 1f, Mathf.Lerp(ripple.color.a, 0, Time.deltaTime * rippleFadeSpeed));
		}
	}
	
	public void click() {
		isPressed = true;
		ripple.rectTransform.localScale = new Vector3(0, 0, 1);
		ripple.transform.position = Input.mousePosition;
		ripple.color = new Color(1f, 1f, 1f, 21 / 255f);
	}
	
	public void stopClick() {
		isPressed = false;
	}
}
