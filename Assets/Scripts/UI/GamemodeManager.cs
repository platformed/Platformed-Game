using UnityEngine;
using System.Collections;

public class GamemodeManager : MonoBehaviour {
	public static GamemodeManager instance;

	public Gamemode Gamemode { get; private set; }
	
	//GameObjects that need to be enabled/disabled for play/design mode
	public GameObject designCam;
	public GameObject playCam;
	public GameObject target;
	public GameObject designCanvas;
	public GameObject playCanvas;
	public GameObject grid;
	public GameObject cursor;
	public GameObject selectBox;

	/// <summary>
	/// True if the UI is transitioning between gamemodes
	/// </summary>
	public bool IsTransitioning { get; private set; }

	void Awake() {
		instance = this;

		Gamemode = Gamemode.Design;
		IsTransitioning = false;
	}

	void Update() {
		if (VRManager.vrMode == VRMode.Disabled) {
			if (Gamemode == Gamemode.Design) {
				//Unlock and show cursor
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;

				//Show design canvas
				playCanvas.SetActive(false);
				designCanvas.SetActive(true);

				//Show design cameraa
				designCam.gameObject.SetActive(true);
				playCam.gameObject.SetActive(false);

				//Show grid
				grid.SetActive(true);

				//Enable target
				target.SetActive(true);

				//Show block cursor and selection box
				cursor.SetActive(true);
				selectBox.SetActive(true);
			}
			if (Gamemode == Gamemode.Play) {
				//Lock and hide cursor
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;

				//Show play canvas
				playCanvas.SetActive(true);
				designCanvas.SetActive(false);

				//Show play camera
				designCam.gameObject.SetActive(false);
				playCam.gameObject.SetActive(true);

				//Hide grid
				grid.SetActive(false);

				//Disable target
				target.SetActive(false);

				//Hide block cursor and select box
				cursor.SetActive(false);
				selectBox.SetActive(false);

				//Press ESC to go back to design mode
				if (Input.GetKey(KeyCode.Escape) && !IsTransitioning) {
					SetGamemode(Gamemode.Design);
				}
			}
		}
	}

	/// <summary>
	/// Set the gamemode
	/// </summary>
	/// <param name="gamemode">The gamemode to set</param>
	public void SetGamemode(Gamemode gamemode) {
		if (Gamemode != gamemode) {
			Gamemode = gamemode;
			//StartTransition();
		} else {
			Gamemode = gamemode;
		}
	}

	void StartTransition() {
		IsTransitioning = true;

		Animator anim = designCanvas.GetComponent<Animator>();

		if (Gamemode == Gamemode.Play) {
			designCam.gameObject.SetActive(true);
			playCam.gameObject.SetActive(false);

			designCam.GetComponent<CameraOrbit>().StartAnimation(playCam.transform.position, playCam.transform.rotation, new Rect(0, 0, 1, 1));

			anim.Play("PlayModeTransition");
		} else if (Gamemode == Gamemode.Design) {
			designCam.gameObject.SetActive(true);
			playCam.gameObject.SetActive(false);

			designCam.GetComponent<CameraOrbit>().Rotation();

			Vector3 endPos = designCam.transform.position;
			Quaternion endRot = designCam.transform.rotation;

			designCam.transform.position = playCam.transform.position;
			designCam.transform.rotation = playCam.transform.rotation;

			designCam.GetComponent<CameraOrbit>().StartAnimation(endPos, endRot, designCam.GetComponent<CameraOrbit>().ViewportRect());

			anim.Play("DesignModeTransition");
		}
	}

	public void StopTransition() {
		IsTransitioning = false;

		if (Gamemode == Gamemode.Play) {
			designCam.gameObject.SetActive(false);
			playCam.gameObject.SetActive(true);
		}
	}
}

public enum Gamemode {
	Play,
	Design
}