using UnityEngine;
using System.Collections;

public class VRMenu : MonoBehaviour {
	public Transform cameraRig;
	public Transform canvas;
	Vector3 previousPos;

	public SteamVR_TrackedObject trackedObject;
	SteamVR_Controller.Device controller;

	Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;

	void Start() {
		previousPos = transform.position;
	}

	void Update() {
		if (controller == null) {
			if ((int)trackedObject.index != -1) {
				controller = SteamVR_Controller.Input((int)trackedObject.index);
			} else {
				return;
			}
		}

		if (controller.GetPress(gripButton)) {
			Vector3 deltaPos = transform.position - previousPos;
			cameraRig.position -= deltaPos;
		}

		if (controller.GetPressDown(triggerButton)) {
			canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy);
		}

		canvas.position = transform.position;
		canvas.rotation = transform.rotation * Quaternion.Euler(90, 0, 0);

		previousPos = transform.position;
	}
}
