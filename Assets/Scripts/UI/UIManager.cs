using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
	public static Tool tool = Tool.BLOCK;
	public Transform blockLibrary;
	public static GameObject world;
	public static bool isDragging = false;
	public static Gamemode gamemode = Gamemode.DESIGN;
	public static string scene;
	public static GameObject tooltip;

	public static int worldSize = 100;

	public static int lives = 3;
	public static float time = 600000;
	public static int score = 0;

	//GameObjects that need to be enabled/disabled for play/design mode
	public static Camera designCam;
	public static Camera playCam;
	public GameObject target;
	public GameObject designCanvas;
	public GameObject playCanvas;
	public GameObject grid;
	public GameObject cursor;
	public GameObject selectBox;

	static bool mouseOverWindow = false;
	static bool navDrawerEnabled = false;

	Animator anim;

	public GameObject windowCanvas;

	void Awake() {
	}

	void Start() {
		//Get camera
		designCam = GameObject.Find("DesignCamera").GetComponent<Camera>();
		playCam = GameObject.Find("PlayCamera").GetComponent<Camera>();

		//Get World
		world = GameObject.Find("World");

		//Get tooltip
		tooltip = Resources.Load("UI/Toolbar/Tooltip") as GameObject;
	}

	void Update() {
		if (gamemode == Gamemode.DESIGN) {
			UnityEngine.Cursor.lockState = CursorLockMode.None;
			UnityEngine.Cursor.visible = true;

			designCanvas.SetActive(true);
			playCanvas.SetActive(false);

			//TODO: FIX THIS
			//designCamera.GetComponent<AudioListener>().enabled = true;
			//playCamera.GetComponent<AudioListener>().enabled = false;

			designCam.gameObject.SetActive(true);
			playCam.gameObject.SetActive(false);
			target.SetActive(true);

			grid.SetActive(true);

			cursor.SetActive(true);
			selectBox.SetActive(true);
		}
		if (gamemode == Gamemode.PLAY) {
			UnityEngine.Cursor.lockState = CursorLockMode.Locked;
			UnityEngine.Cursor.visible = false;

			designCanvas.SetActive(false);
			playCanvas.SetActive(true);

			//designCamera.GetComponent<AudioListener>().enabled = false;
			//playCamera.GetComponent<AudioListener>().enabled = true;

			designCam.gameObject.SetActive(false);
			playCam.gameObject.SetActive(true);
			target.SetActive(false);

			grid.SetActive(false);

			cursor.SetActive(false);
			selectBox.SetActive(false);

			//Press ESC to go back to design mode
			if (Input.GetKey(KeyCode.Escape)) {
				gamemode = Gamemode.DESIGN;
			}
		}
	}

	void FixedUpdate() {
	}

	public static void setToolBlock(string name) {
		tool = Tool.BLOCK;
		Cursor.SetBlock(new Block[,,] { { { BlockManager.GetBlock(name) } } });
		Cursor.offset = Vector3.zero;
	}

	//Returns weather the user can interact with the level
	public static bool canInteract() {
		if (isDragging) {
			return false;
		}
		if (Input.mousePosition.y > Screen.height - 64 || Input.mousePosition.x > Screen.width - 200) {
			return false;
		}
		if (mouseOverWindow) {
			return false;
		}
		if (navDrawerEnabled) {
			return false;
		}
		if (CategorySelector.isVisible) {
			return false;
		}
		return true;
	}

	public static void pointerEnter() {
		mouseOverWindow = true;
	}

	public static void pointerExit() {
		mouseOverWindow = false;
	}

	public static void NavDrawerEnabled(bool b) {
		navDrawerEnabled = b;
	}

	public static string getTime() {
		//TODO: format time
		return "10:00";
	}

	//Raycasts from the mouse cursor to the plane that the camera target is on
	public static Vector3 raycast() {
		Plane plane = new Plane(Vector3.up, new Vector3(0, CameraMove.floor, 0));

		Vector3 hit;
		Ray ray = designCam.ScreenPointToRay(Input.mousePosition);
		ray.direction = ray.direction * 1000;
		float distance;
		if (plane.Raycast(ray, out distance)) {
			hit = ray.GetPoint(distance);
			//Debug.Log("raycast: " + hit.ToString());
			return hit;
		}

		return new Vector3();
	}

	//Gets the scene that the level is loading
	public static string getScene() {
		return scene;
	}

	//Puts the game into the loading screen to load a level
	public static void loadScene(string s) {
		//scene = s;
		//SceneManager.LoadScene("loading-screen");
		SceneManager.LoadSceneAsync(s);
	}
}

public enum Tool {
	SELECT,
	BLOCK,
	PAN,
	ORBIT,
	ZOOM
}

public enum Gamemode {
	PLAY,
	DESIGN
}