using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
	public static Tool tool = Tool.SELECT;
	public Transform blockLibrary;
	public GameObject world;
	public static bool isDraging = false;
	public static string scene;
	public static string version = "Alpha v0.0.0";

	static bool mouseOverWindow = false;

	Animator anim;
	bool levelSaved = true;
	GameObject blockButton;

	void Start() {
		//Add block buttons to bottom
		blockButton = Resources.Load ("UI Elements/BlockButton") as GameObject;
		foreach(Block block in Block.blocks){
			if(block.getID() != 0){
				GameObject button = Instantiate (blockButton) as GameObject;
				button.transform.SetParent(blockLibrary);
				button.name = "BlockButton" + block.getName();

				Button b = button.GetComponent<Button>();
				int id = block.getID();
				b.onClick.AddListener(() => setToolBlock(id));

				Text name = button.GetComponentInChildren<Text>();
				name.text = block.getName();
			}
		}
	}
	
	void Update() {

	}

	//Returns weather the user can interact with the level
	public static bool canInteract() {
		if (isDraging)
			return false;
		if (Input.mousePosition.y > Screen.height - 48 || Input.mousePosition.x < 200)
			return false;
		if (mouseOverWindow)
			return false;
		return true;
	}

	public void setTool(int t) {
		switch (t) {
			case 0:
				tool = Tool.SELECT;
				break;
			case 1:
				tool = Tool.BLOCK;
				break;
			case 2:
				tool = Tool.PAN;
				break;
			case 3:
				tool = Tool.ORBIT;
				break;
			case 4:
				tool = Tool.ZOOM;
				break;
		}
	}

	public void setToolBlock(int id){
		setTool (1);
		Cursor.block = Block.blocks [id];
	}

	public void pointerEnter() {
		mouseOverWindow = true;
	}

	public void pointerExit() {
		mouseOverWindow = false;
	}

	public void exitToMenu() {
		if(levelSaved){
			Application.LoadLevel("main-menu");
		}
	}

	public void newLevel() {

	}

	public void openLevel() {
		//world.GetComponent<World>().loadWorld("testSave2.level");
	}

	public void saveLevel() {
		//world.GetComponent<World>().saveWorld("testSave2.level");
	}

	public void levelSettings() {

	}

	public void rotateBlock() {

	}

	public void blockProperties() {

	}

	public void playLevel() {

	}

	public void uploadLevel() {

	}

	public void upFloor() {
		CameraMove.floor++;
	}

	public void downFloor() {
		CameraMove.floor--;
	}

	//Raycasts from the mouse cursor to the plane that the camera target is on
	public static Vector3 raycast() {
		Plane plane = new Plane(Vector3.up, new Vector3(0, CameraMove.floor, 0));
		
		Vector3 hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		ray.direction = ray.direction * 1000;
		float distance;
		if (plane.Raycast(ray, out distance)) {
			hit = ray.GetPoint(distance);
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

	public static string getVersion() {
		return version;
	}
}

public enum Tool {
	SELECT,
	BLOCK,
	PAN,
	ORBIT,
	ZOOM
}