using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour {
	Vector3 pos1;
	Vector3 pos2;
	const float offset = 0.01f;

	void Update() {
		if (UIManager.tool == Tool.SELECT && UIManager.canInteract()) {
			if (Input.GetMouseButtonDown(0)) {
				pos1 = Round(UIManager.raycast());
			}
			if (Input.GetMouseButton(0)) {
				pos2 = Round(UIManager.raycast());
			}
		}

		Render();
	}

	void Render() {
		transform.position = new Vector3((pos1.x + pos2.x) / 2, (pos1.y + pos2.y) / 2, (pos1.z + pos2.z) / 2);

		float x = pos2.x - pos1.x;
		float y = pos2.y - pos1.y;
		float z = pos2.z - pos1.z;

		//Add extra scale
		if (x > 0) {
			x += 1 + offset;
		} else {
			x -= 1 + offset;
		}

		if (y > 0) {
			y += 1 + offset;
		} else {
			y -= 1 + offset;
		}

		if (z > 0) {
			z += 1 + offset;
		} else {
			z -= 1 + offset;
		}

		transform.localScale = new Vector3(x, y, z);
	}

	Vector3 Round(Vector3 v) {
		return new Vector3((int)v.x + 0.5f, (int)v.y + 0.5f, (int)v.z + 0.5f);
	}
}
