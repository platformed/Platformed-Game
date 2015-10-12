using UnityEngine;
using System.Collections;

public class SelectBox : MonoBehaviour {
	Vector3 p1;
	Vector3 p2;
	//bool selected = false;
	
	void Start () {
		
	}
	
	void Update () {
		if (UIManager.gamemode == Gamemode.DESIGN) {
			gameObject.SetActive(true);

			if (UIManager.tool == Tool.SELECT && UIManager.canInteract()) {
				if (Input.GetMouseButtonDown(0)) {
					p1 = round1(UIManager.raycast());
					//selected = true;
				}
				if (Input.GetMouseButton(0)) {
					p2 = round2(UIManager.raycast());
				}
			}

			if (UIManager.tool != Tool.SELECT) {
				//selected = false;
			}

			setPosition();
		} else {
			//Hide in play mode
			gameObject.SetActive(false);
		}
	}

	void setPosition(){
		transform.position = new Vector3 ((p1.x + p2.x) / 2, (p1.y + p2.y) / 2, (p1.z + p2.z) / 2);
		transform.localScale = new Vector3 (p2.x - p1.x + 0.01f, p2.y - p1.y + 0.01f, p2.z - p1.z + 0.01f);

		/*if (corner) {
			transform.position = p1;
		} else {
			transform.position = p2;
		}*/
	}

	Vector3 round1(Vector3 v){
		int x = (int) Mathf.Floor (v.x);
		int y = (int) Mathf.Floor (v.y);
		int z = (int) Mathf.Floor (v.z);

		return new Vector3 (x, y, z);
	}

	Vector3 round2(Vector3 v){
		int x = (int) Mathf.Floor (v.x);
		int y = (int)Mathf.Floor(v.y);
		int z = (int) Mathf.Floor (v.z);

		if (v.x >= p1.x) {
			x++;
		}
		if (v.y >= p1.y) {
			y++;
		}
		if (v.z >= p1.z) {
			z++;
		}

		return new Vector3 (x, y, z);
	}
}
