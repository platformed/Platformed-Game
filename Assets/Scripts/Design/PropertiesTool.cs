using UnityEngine;
using System.Collections;

public class PropertiesTool : MonoBehaviour {
	public Camera designCam;
	Block block;

	void Start() {

	}

	void Update() {
		if (UIManager.canInteract() && UIManager.tool == Tool.Properties) {
			if (Input.GetMouseButtonDown(0)) {
				Block b = Raycast();
				if (b != null) {
					block = b;
					Debug.Log(block.GetDisplayName());
				}
			}
		}
	}

	Block Raycast() {
		//Get ray
		Ray ray = designCam.ScreenPointToRay(Input.mousePosition);

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
