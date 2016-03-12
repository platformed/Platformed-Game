using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputFieldController : MonoBehaviour {
	public Image underline;
	public InputField inputField;

	Color underlineColor;
	Color underlineColorSelected;
	float speed = 10f;

	void Start () {
		underlineColor = new Color(0f, 0f, 0f, 0.38f);
		underlineColorSelected = underline.color;
		Debug.Log(underline.rectTransform.sizeDelta.ToString());
	}
	
	void Update () {
		if (inputField.isFocused) {
			underline.rectTransform.sizeDelta = Vector2.Lerp(underline.rectTransform.sizeDelta, new Vector2(0, 2f), Time.deltaTime * speed);
			underline.color = Color.Lerp(underline.color, underlineColorSelected, Time.deltaTime * speed);
		} else {
			underline.rectTransform.sizeDelta = Vector2.Lerp(underline.rectTransform.sizeDelta, new Vector2(0, 1f), Time.deltaTime * speed);
			underline.color = Color.Lerp(underline.color, underlineColor, Time.deltaTime * speed);
		}
	}
}
