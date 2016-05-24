using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class Dialog : MonoBehaviour {
	RectTransform rectTransform;

	Graphic shadow;
	Graphic shadowHover;
	const float shadowFadeDuration = 0.15f;

	const float fadeDuration = 0.1f;
	Vector3 mouseOffset;

	bool closing = false;

	void Start() {
		rectTransform = GetComponent<RectTransform>();

		//Create shadows
		shadow = Instantiate(Resources.Load("UI/Shadows/Shadow-3") as GameObject).GetComponent<Graphic>();
		shadowHover = Instantiate(Resources.Load("UI/Shadows/Shadow-5") as GameObject).GetComponent<Graphic>();

		//Set parents
		shadow.transform.SetParent(transform.parent);
		shadowHover.transform.SetParent(transform.parent);

		//Set names
		shadow.transform.name = transform.name + " Shadow";
		shadowHover.transform.name = transform.name + " Hover Shadow";

		//Order transforms
		shadow.transform.SetAsLastSibling();
		shadowHover.transform.SetAsLastSibling();
		transform.SetAsLastSibling();

		//Hide at first
		FadeOut(0f);
		shadow.CrossFadeAlpha(0f, 0f, false);
		shadowHover.CrossFadeAlpha(0f, 0f, false);

		//Fade in
		FadeIn(fadeDuration);
		shadow.CrossFadeAlpha(1f, fadeDuration, false);
	}

	void Update() {
		Rect rect = new Rect(rectTransform.position.x, rectTransform.position.y, rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);

		rect.size += new Vector2(500f, 500f);

		shadow.rectTransform.position = rect.position;
		shadow.rectTransform.sizeDelta = rect.size;
		shadowHover.rectTransform.position = rect.position;
		shadowHover.rectTransform.sizeDelta = rect.size;
	}

	public void CloseDialog() {
		closing = true;

		UIManager.pointerExit();

		FadeOut(fadeDuration);

		shadow.CrossFadeAlpha(0f, fadeDuration, false);
		shadowHover.CrossFadeAlpha(0f, fadeDuration, false);

		Destroy(gameObject, fadeDuration);
		Destroy(shadow.gameObject, fadeDuration);
		Destroy(shadowHover.gameObject, fadeDuration);
	}

	public void StartDrag() {
		shadow.transform.SetAsLastSibling();
		shadowHover.transform.SetAsLastSibling();
		transform.SetAsLastSibling();

		mouseOffset = transform.position - Input.mousePosition;
		if (!closing) {
			UIManager.isDragging = true;
		}

		shadow.CrossFadeAlpha(0f, shadowFadeDuration, false);
		shadowHover.CrossFadeAlpha(1f, shadowFadeDuration, false);
	}

	public void StopDrag() {
		if (!closing) {
			UIManager.isDragging = false;
		}

		shadow.CrossFadeAlpha(1f, shadowFadeDuration, false);
		shadowHover.CrossFadeAlpha(0f, shadowFadeDuration, false);
	}

	public void Drag() {
		transform.position = Input.mousePosition + mouseOffset;
		ClampPos();
	}

	public void PointerEnter() {
		if (!closing) {
			UIManager.pointerEnter();
		}
	}

	public void PointerExit() {
		if (!closing) {
			UIManager.pointerExit();
		}
	}

	void ClampPos() {
		if (transform.position.x < 0 + (GetWidth() / 2)) {
			transform.position = new Vector3(GetWidth() / 2, transform.position.y, 0);
		}

		if (transform.position.x > Screen.width - (GetWidth() / 2)) {
			transform.position = new Vector3(Screen.width - (GetWidth() / 2), transform.position.y, 0);
		}

		if (transform.position.y < 0 + (GetHeight() / 2)) {
			transform.position = new Vector3(transform.position.x, GetHeight() / 2, 0);
		}

		if (transform.position.y > Screen.height - (GetHeight() / 2)) {
			transform.position = new Vector3(transform.position.x, Screen.height - (GetHeight() / 2), 0);
		}
	}

	float GetWidth() {
		return rectTransform.rect.width;
	}

	float GetHeight() {
		return rectTransform.rect.height;
	}

	void FadeIn(float duration) {
		Graphic[] images = GetComponentsInChildren<Graphic>();
		foreach (Graphic i in images) {
			i.CrossFadeAlpha(1f, duration, false);
		}
	}

	void FadeOut(float duration) {
		Graphic[] images = GetComponentsInChildren<Graphic>();
		foreach (Graphic i in images) {
			i.CrossFadeAlpha(0f, duration, false);
		}
	}

	public void SetParent(Transform parent) {
		shadow.transform.SetParent(parent);
		shadowHover.transform.SetParent(parent);
		transform.SetParent(parent);
	}
}
