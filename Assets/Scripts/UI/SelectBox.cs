using UnityEngine;
using System.Collections;

public class SelectBox : MonoBehaviour {
	Vector3 start;
	Vector3 end;
	Vector3 p1;
	Vector3 p2;
	
	public World world;
	Block[,,] clipboard;

	void Update() {
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

	int lower(float a, float b) {
		if (a < b)
			return Mathf.FloorToInt(a);
		else
			return Mathf.FloorToInt(b);
	}

	int higher(float a, float b) {
		if (a > b)
			return Mathf.FloorToInt(a);
		else
			return Mathf.FloorToInt(b);
	}

	void setPosition() {
		transform.position = new Vector3((p1.x + p2.x) / 2, (p1.y + p2.y) / 2, (p1.z + p2.z) / 2);
		transform.localScale = new Vector3(p2.x - p1.x, p2.y - p1.y, p2.z - p1.z);
	}

	Vector3 round1(Vector3 v) {
		float x = (int)Mathf.Floor(v.x);
		float y = (int)Mathf.Floor(v.y);
		float z = (int)Mathf.Floor(v.z);

		if (v.x > p2.x) {
			x += 1.01f;
		} else {
			x -= 0.01f;
		}

		if (v.y > p2.y) {
			y += 1.01f;
		} else {
			y -= 0.01f;
		}

		if (v.z > p2.z) {
			z += 1.01f;
		} else {
			z -= 0.01f;
		}

		return new Vector3(x, y, z);
	}

	Vector3 round2(Vector3 v) {
		float x = (int)Mathf.Floor(v.x);
		float y = (int)Mathf.Floor(v.y);
		float z = (int)Mathf.Floor(v.z);

		if (v.x >= p1.x) {
			x += 1.01f;
		} else {
			x -= 0.01f;
		}

		if (v.y >= p1.y) {
			y += 1.01f;
		} else {
			y -= 0.01f;
		}

		if (v.z >= p1.z) {
			z += 1.01f;
		} else {
			z -= 0.01f;
		}

		return new Vector3(x, y, z);
	}
}
