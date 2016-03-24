using UnityEngine;
using System.Collections;

public class GridRenderer : MonoBehaviour {
	public Material gridMaterial;
	public Material overlayMaterial;
	public static bool isVisible = true;
	float offset = 0.01f;
	bool visible;

	void Start () {
		//Set position to the center of the world
		transform.position = new Vector3 (UIManager.worldSize / 2, CameraMove.floor + offset, UIManager.worldSize / 2);
	}

	void Update () {
		//Smooth transition with lerp
		transform.position = new Vector3 (transform.position.x, Mathf.Lerp(transform.position.y, CameraMove.floor + offset, Time.deltaTime * CameraMove.smooth), transform.position.z);

		gridMaterial.color = Color.Lerp(gridMaterial.color, isVisible ? Color.black : Color.clear, Time.deltaTime * 15);
		overlayMaterial.color = Color.Lerp(overlayMaterial.color, isVisible ? Color.black : Color.clear, Time.deltaTime * 15);

		/*if(isVisible) {
			MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
			foreach(MeshRenderer r in renderers) {
				r.enabled = false;
			}
		} else {
			MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer r in renderers) {
				r.enabled = true;
			}
		}*/
	}

	public static void ToggleGrid() {
		isVisible = !isVisible;
	}
}