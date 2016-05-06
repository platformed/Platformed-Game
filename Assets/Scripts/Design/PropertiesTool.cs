using UnityEngine;
using System.Collections;

public class PropertiesTool : MonoBehaviour {
	public Camera designCam;

	void Start() {

	}

	void Update() {
		if (UIManager.canInteract() && UIManager.tool == Tool.Properties) {
			if (Input.GetMouseButtonDown(0)) {
				Block b = Raycast();
				if (b != null) {
					Debug.Log(b.GetDisplayName());
				}
			}
		}
	}

	Block Raycast() {
		//Get ray
		Ray ray = designCam.ScreenPointToRay(Input.mousePosition);
		ray.direction *= 1000f;

		//Raycast
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			if (hit.transform.GetComponent<Chunk>() != null) {
				return hit.transform.GetComponent<Chunk>().GetBlockFromCollider(hit.collider);
			}
		}

		return null;
	}
}
