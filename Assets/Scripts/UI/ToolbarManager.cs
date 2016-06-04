using UnityEngine;
using System.Collections;

public class ToolbarManager : MonoBehaviour {
	public SelectTool selectBox;
	public World world;
	public Transform windowCanvas;

	GameObject saveDialog;
	GameObject saveDialogPrefab;

	GameObject openDialog;
	GameObject openDialogPrefab;

	void Start() {
		saveDialogPrefab = Resources.Load("UI/Dialog/Save Dialog") as GameObject;
		openDialogPrefab = Resources.Load("UI/Dialog/Open Dialog") as GameObject;
	}

	public void setTool(int t) {
		switch (t) {
			case 0:
				UIManager.tool = Tool.Select;
				break;
			case 1:
				UIManager.tool = Tool.Block;
				break;
			case 2:
				UIManager.tool = Tool.Pan;
				break;
			case 3:
				UIManager.tool = Tool.Orbit;
				break;
			case 4:
				UIManager.tool = Tool.Zoom;
				break;
			case 5:
				UIManager.tool = Tool.Properties;
				break;
		}
	}

	public static void save(string name) {
		//UIManager.world.GetComponent<World>().saveWorld(name + ".level");
	}

	public static void load(string name) {
		//UIManager.world.GetComponent<World>().loadWorld(name + ".level");
	}

	public void newLevel() {
		world.ClearWorld();
	}

	public void openLevel() {
		if (openDialog == null) {
			openDialog = Instantiate(openDialogPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f), Quaternion.identity) as GameObject;
			openDialog.transform.SetParent(windowCanvas);
			openDialog.transform.SetAsLastSibling();
		}
	}

	public void saveLevel() {
		if (saveDialog == null) {
			saveDialog = Instantiate(saveDialogPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f), Quaternion.identity) as GameObject;
			saveDialog.transform.SetParent(windowCanvas);
			saveDialog.transform.SetAsLastSibling();
		}
	}

	public void levelSettings() {

	}

	public void copy() {
		selectBox.GetComponent<SelectTool>().Copy();
		BlockCursor.offset = Vector3.zero;
	}

	public void toggleGrid() {
		GridRenderer.ToggleGrid();
	}

	public void playLevel() {
		UIManager.instance.SetGamemode(Gamemode.Play);
	}

	public void uploadLevel() {

	}

	public void upFloor() {
		CameraMove.floor++;
	}

	public void downFloor() {
		CameraMove.floor--;
	}
}
