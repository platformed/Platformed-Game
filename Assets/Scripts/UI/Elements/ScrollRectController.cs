using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ScrollRect))]
public class ScrollRectController : MonoBehaviour {
	ScrollRect scrollRect;
	RectTransform scrollRectTransform;
	RectTransform scrollRectContent;

	float velocity = 0f;
	const float decelerationRate = 1f - 0.135f;
	const float scrollSpeed = 100f;

	void Start () {
		scrollRect = GetComponent<ScrollRect>();
		scrollRectTransform = scrollRect.GetComponent<RectTransform>();
		scrollRectContent = scrollRect.content;
    }

	void Update() {
		//Add scrollwheel to velocity
		if (EventSystem.current.IsPointerOverGameObject()) {
			velocity += (Input.GetAxis("Mouse ScrollWheel") * scrollSpeed);
		}

		//Slow velocity over time
		velocity *= decelerationRate;

		//Normalize velocity
		float normalizedVelocity = (velocity / (scrollRectContent.rect.height - scrollRectTransform.rect.height));

		//Add normalized velocity to the scroll height
        scrollRect.verticalNormalizedPosition += normalizedVelocity;

		//Clamp the scroll height
		scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
	}
}
