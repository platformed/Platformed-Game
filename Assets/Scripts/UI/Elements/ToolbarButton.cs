using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToolbarButton : MonoBehaviour {
	public Image ripple;
	public string tooltipText;

	bool isPressed = false;
	bool isHovering = false;
	float hoverTime = 0f;

	float rippleSpeed = 16;
	float rippleFadeSpeed = 4;

	GameObject tooltip = null;

	void Start() {
		ripple.rectTransform.localScale = new Vector3(0, 0, 1);
	}

	void Update() {
		ripple.rectTransform.localScale = Vector3.Lerp(ripple.rectTransform.localScale, new Vector3(1, 1, 1), Time.deltaTime * rippleSpeed);
		if (!isPressed) {
			ripple.color = new Color(1f, 1f, 1f, Mathf.Lerp(ripple.color.a, 0, Time.deltaTime * rippleFadeSpeed));
		}

		if (isHovering) {
			hoverTime += Time.deltaTime;
		}
		if(hoverTime >= 1.5f && tooltip == null && tooltipText != "") {
			tooltip = Instantiate(UIManager.tooltip, transform.position - new Vector3(0, 28, 0), Quaternion.identity) as GameObject;
			tooltip.GetComponentInChildren<Text>().text = tooltipText;
			tooltip.transform.SetParent(transform);
			tooltip.name = "Tooltip";
		}
	}

	public void hover() {
		isHovering = true;
	}

	public void stopHover() {
		isHovering = false;
		hoverTime = 0f;
		Destroy(tooltip);
	}

	public void click() {
		isPressed = true;
		ripple.rectTransform.localScale = new Vector3(0, 0, 1);
		ripple.color = new Color(1f, 1f, 1f, 21 / 255f);
	}

	public void stopClick() {
		isPressed = false;
	}
}
