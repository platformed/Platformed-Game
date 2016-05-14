using UnityEngine;
using System.Collections;

public class VRManager : MonoBehaviour {
	public static VRMode vrMode = VRMode.RoomScale;

	public GameObject cameraRig;
	public GameObject vrCursor;

	public GameObject designCamera;
	public GameObject playCamera;
	public GameObject blockCursor;
	public GameObject target;
	public GameObject designCanvas;
	public GameObject grid;

	void Start() {
		if (vrMode == VRMode.RoomScale) {
			cameraRig.SetActive(true);
			vrCursor.SetActive(true);

			designCamera.SetActive(false);
			playCamera.SetActive(false);
			blockCursor.SetActive(false);
			target.SetActive(false);
			designCanvas.SetActive(false);
			grid.SetActive(false);
		}
	}

	void Update() {

	}
}

public enum VRMode {
	Disabled,
	RoomScale
}