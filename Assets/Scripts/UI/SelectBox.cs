using UnityEngine;
using System.Collections;

public class SelectBox : MonoBehaviour {
	Vector3 start;
	Vector3 end;
	Vector3 p1;
	Vector3 p2;

	void Start() {

	}

	void Update() {
		gameObject.SetActive(true);

		if (UIManager.tool == Tool.SELECT && UIManager.canInteract()) {
			if (Input.GetMouseButtonDown(0)) {
				start = UIManager.raycast();
			}
			if (Input.GetMouseButton(0)) {
				end = UIManager.raycast();
			}
		}

		p1 = round1(start);
		p2 = round2(end);

		setPosition();
	}

	void setPosition() {
		transform.position = new Vector3((p1.x + p2.x) / 2, (p1.y + p2.y) / 2, (p1.z + p2.z) / 2);
		transform.localScale = new Vector3(p2.x - p1.x, p2.y - p1.y, p2.z - p1.z);
	}

	Vector3 round1(Vector3 v) {
		int x = (int)Mathf.Floor(v.x);
		int y = (int)Mathf.Floor(v.y);
		int z = (int)Mathf.Floor(v.z);

		if (v.x > p2.x) {
			x++;
		}
		if (v.y > p2.y) {
			y++;
		}
		if (v.z > p2.z) {
			z++;
		}

		return new Vector3(x, y, z);
	}

	Vector3 round2(Vector3 v) {
		int x = (int)Mathf.Floor(v.x);
		int y = (int)Mathf.Floor(v.y);
		int z = (int)Mathf.Floor(v.z);

		if (v.x >= p1.x) {
			x++;
		}
		if (v.y >= p1.y) {
			y++;
		}
		if (v.z >= p1.z) {
			z++;
		}
		
		return new Vector3(x, y, z);
	}
}
