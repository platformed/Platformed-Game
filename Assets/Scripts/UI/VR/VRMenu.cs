using UnityEngine;
using System.Collections;

public class VRMenu : MonoBehaviour {
	public Transform cameraRig;
	public Transform canvas;
	Vector3 previousPos;
	Vector3 test = Vector3.zero;

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

			/*if (Vector3.Distance(transform.position, test) > 0.01f) {
				Debug.Log("triggered pulse " + transform.position.ToString() + " " + test.ToString());
				controller.TriggerHapticPulse(1000);
				test = transform.position;
			} else {
				Debug.Log(Vector3.Distance(transform.position, test));
			}*/
		}

		if (controller.GetPressDown(triggerButton)) {
			canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy);
		}

		canvas.position = transform.position;
		canvas.rotation = transform.rotation * Quaternion.Euler(90, 0, 0);

		previousPos = transform.position;
	}
}
