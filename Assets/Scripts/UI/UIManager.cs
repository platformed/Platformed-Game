using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
	public static Tool tool = Tool.SELECT;
	public Transform blockLibrary;
	public static GameObject world;
	public static bool isDraging = false;
	public static Gamemode gamemode = Gamemode.DESIGN;
	public static string scene;

	public static int lives = 3;
	public static float time = 600000;
	public static int score = 0;

	//GameObjects that need to be enabled/disabled for play/design mode
	public static Camera designCam;
	public static Camera playCam;
	public GameObject target;
	public GameObject designCanvas;
	public GameObject playCanvas;
	public GameObject grid1;
	public GameObject grid2;
	public GameObject grid3;
	public GameObject cursor;
	public GameObject selectBox;

	static bool mouseOverWindow = false;

	Animator anim;
	bool levelSaved = true;
	GameObject blockButton;

	public GameObject windowCanvas;
	GameObject saveWindow;

	void Awake() {
	}

	void Start() {
		//Add blocks to block list
		Block.addBlock(new AirBlock());
		Block.addBlock(new GreenStoneBlock());
		Block.addBlock(new StoneBlock());
		Block.addBlock(new DirtBlock());
		Block.addBlock(new SandcastleWallBlock());
		Block.addBlock(new TestBlock1Block());
		Block.addBlock(new TestBlock2Block());
		Block.addBlock(new TestBlock3Block());

		//Get camera
		designCam = GameObject.Find("DesignCamera").GetComponent<Camera>();
		playCam = GameObject.Find("PlayCamera").GetComponent<Camera>();

		//Get World
		world = GameObject.Find("World");

		//Get windows
		saveWindow = Resources.Load("UI Elements/SaveWindow") as GameObject;

		//Add block buttons to bottom
		blockButton = Resources.Load("UI Elements/BlockButton") as GameObject;
		foreach (BlockType block in Block.getBlocks()) {
			if (!block.getName().Equals("Air")) {
				GameObject button = Instantiate(blockButton) as GameObject;
				button.transform.SetParent(blockLibrary);
				button.name = "BlockButton" + block.getName();

				Button b = button.GetComponent<Button>();
				string n = block.getName();
				b.onClick.AddListener(() => setToolBlock(n));

				Text name = button.GetComponentInChildren<Text>();
				name.text = block.getDisplayName();
			}
		}
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

			grid1.SetActive(true);
			grid2.SetActive(true);
			grid3.SetActive(true);

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

			grid1.SetActive(false);
			grid2.SetActive(false);
			grid3.SetActive(false);

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

	public void setToolBlock(string name) {
		tool = Tool.BLOCK;
		Cursor.block = new Block[,,] { { { Block.newBlock(name) } } };
	}

	//Returns weather the user can interact with the level
	public static bool canInteract() {
		if (isDraging)
			return false;
		if (Input.mousePosition.y > Screen.height - 56 || Input.mousePosition.x > Screen.width - 200)
			return false;
		if (mouseOverWindow)
			return false;
		return true;
	}

	public void pointerEnter() {
		mouseOverWindow = true;
	}

	public void pointerExit() {
		mouseOverWindow = false;
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
		scene = s;
		Application.LoadLevel("loading-screen");
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