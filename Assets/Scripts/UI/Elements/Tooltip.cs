using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
	Vector3 target;
	Vector3 shown;
	Vector3 hidden;

	void Start () {
		shown = transform.position;
		hidden = new Vector3(transform.position.x, transform.position.y + 22, transform.position.z);

		target = shown;
		transform.position = hidden;
	}
	
	void Update () {
		transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 16);
		if(target == hidden && transform.position.y < hidden.y - 0.1) {
			Destroy(gameObject);
		}
	}

	public void hide() {
		target = hidden;
	}
}
