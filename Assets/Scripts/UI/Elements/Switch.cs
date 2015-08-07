using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Switch : MonoBehaviour {
	public Image thumb;
	public Image track;
	public Color color;

	bool value;

	Vector3 thumbPos = new Vector3(-26f, 0f, 0f);
	Vector3 mouseOffset;
	bool dragging = false;

	float speed = 16;
	float colorSpeed = 24;

	void Start () {
		thumb.transform.localPosition = thumbPos;
	}

	void Update () {
		if (value) {
			thumbPos = new Vector3 (-26f, 0f, 0f);
		} else {
			thumbPos = new Vector3(-50f, 0f, 0f);
		}

		setColors ();
		setPos ();
	}

	public void click(){
		value = !value;
	}

	/*public void drag(){
		Vector3 pos = new Vector3 (Input.mousePosition.x + mouseOffset.x, 0f, 0f);

		//Clamp pos
		if(pos.x < -50f) {
			pos = new Vector3 (-50f, 0f, 0f);
		}
		if(pos.x > -26f) {
			pos = new Vector3 (-26f, 0f, 0f);
		}

		thumb.transform.localPosition = pos;
	}

	public void startDrag(){
		if (Input.mousePosition != mousePos) {
			dragging = true;
			mouseOffset = thumb.transform.localPosition - Input.mousePosition;
		}

		mousePos = Input.mousePosition;
	}

	public void stopDrag(){
		dragging = false;

		if (thumb.transform.localPosition.x > -38) {
			value = true;
		} else {
			value = false;
		}
	}*/

	void setColors() {
		if(value) {
			thumb.color = Color.Lerp(thumb.color, color, Time.deltaTime * colorSpeed);
			track.color = new Color(color.r, color.g, color.b, 0.5f);
		} else {
			thumb.color = Color.Lerp(thumb.color, new Color(0.980392157f, 0.980392157f, 0.980392157f), Time.deltaTime * colorSpeed);
			track.color = new Color(0f, 0f, 0f, 0.26f);
		}
	}

	void setPos() {
		thumb.transform.localPosition = Vector3.Lerp (thumb.transform.localPosition, thumbPos, Time.deltaTime * speed);
	}
}
