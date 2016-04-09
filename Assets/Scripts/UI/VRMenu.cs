using UnityEngine;
using System.Collections;

public class VRMenu : MonoBehaviour {
	public Transform cameraRig;
	Vector3 previousPos;

	public SteamVR_TrackedObject trackedObject;
	SteamVR_Controller.Device controller;

	Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;

	void Start() {
		controller = SteamVR_Controller.Input((int)trackedObject.index);

		previousPos = transform.position;
	}

	void Update() {
		if (controller.GetPress(gripButton)) {
			Vector3 deltaPos = transform.position - previousPos;
			cameraRig.position -= deltaPos;
		}

		previousPos = transform.position;
	}
}
