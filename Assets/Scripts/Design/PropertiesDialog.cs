using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
public class PropertiesDialog : MonoBehaviour {
	public Text title;
	public RectTransform circleTransform;

	Vector2[] points;
	float duration;
	float circleDuration;
	AnimationCurve speedCurve;
	AnimationCurve circleCurve;

	public void StartAnimation(Vector2 start, Vector2 end, AnimationCurve speedCurve, AnimationCurve circleCurve, float duration, float circleDuration) {
		this.speedCurve = speedCurve;
		this.circleCurve = circleCurve;
		this.duration = duration;
		this.circleDuration = circleDuration;

		float xDistance = Mathf.Abs(start.x - end.x);
		float yDistance = Mathf.Abs(start.y - end.y);

		float handleDistance = xDistance < yDistance ? xDistance : yDistance;
		points = new Vector2[] {
			start,
			start.y > end.y ? start + Vector2.down * handleDistance : (start.x > end.x ? start + Vector2.left * handleDistance : start + Vector2.right * handleDistance),
			start.y < end.y ? end + Vector2.down * handleDistance : (start.x > end.x ? end + Vector2.right * handleDistance : end + Vector2.left * handleDistance),
			end
		};

		//Debug points
		/*GameObject prefab = Resources.Load("Debug Point") as GameObject;
		Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        foreach (Vector2 p in points) {
			GameObject instance = Instantiate(prefab, p, Quaternion.identity) as GameObject;
			instance.transform.SetParent(transform.parent);
			instance.transform.SetAsLastSibling();
			instance.GetComponent<Graphic>().color = color;
		}*/

		StartCoroutine(Animate());
		StartCoroutine(AnimateCircle());
	}

	IEnumerator Animate() {
		RectTransform rectTransform = GetComponent<RectTransform>();
		Vector2 size = rectTransform.sizeDelta;

		Vector2[] scalePoints = new Vector2[] {
			new Vector2(1f, 0.1f),
			new Vector2(1f, 0.2f),
			new Vector2(1f, 0.9f),
			new Vector2(1f, 1f)
		};

		for (float i = 0; i < duration; i += Time.deltaTime) {
			float distance = speedCurve.Evaluate(i / duration);

			if (circleTransform != null) {
				circleTransform.position = BezierCurve2D.Evaluate(points, distance);
			} else {
				transform.position = BezierCurve2D.Evaluate(points, distance);
			}

			Vector2 sizeMultiplyer = BezierCurve2D.Evaluate(scalePoints, distance);
            rectTransform.sizeDelta = new Vector2(size.x * sizeMultiplyer.x, size.y * sizeMultiplyer.y);

			yield return null;
		}
	}

	IEnumerator AnimateCircle() {
		Vector2 size = circleTransform.sizeDelta;

		for (float i = 0; i < circleDuration; i += Time.deltaTime) {
			float distance = circleCurve.Evaluate(i / circleDuration);

			circleTransform.sizeDelta = size * distance;

			yield return null;
		}

		GetComponent<Dialog>().SetParent(transform.parent.parent);
		Destroy(circleTransform.gameObject);
	}

	public void SetTitle(string title) {
		this.title.text = title + " Properties";
	}

	public void SetProperties(List<BlockProperty> properties) {
		/*foreach(BlockProperty p in properties){

		}*/
	}
}
