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

	/// <summary>
	/// Sets the tool
	/// </summary>
	/// <param name="tool">The id of the tool to set</param>
	public void SetTool(int tool) {
		switch (tool) {
			case 0:
				DesignManager.instance.tool = Tool.Select;
				break;
			case 1:
				DesignManager.instance.tool = Tool.Block;
				break;
			case 2:
				DesignManager.instance.tool = Tool.Pan;
				break;
			case 3:
				DesignManager.instance.tool = Tool.Orbit;
				break;
			case 4:
				DesignManager.instance.tool = Tool.Zoom;
				break;
			case 5:
				DesignManager.instance.tool = Tool.Properties;
				break;
		}
	}

	/// <summary>
	/// Clears the world
	/// </summary>
	public void NewLevel() {
		world.ClearWorld();
	}

	/// <summary>
	/// Shows the open dialog
	/// </summary>
	public void OpenLevel() {
		if (openDialog == null) {
			openDialog = Instantiate(openDialogPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f), Quaternion.identity) as GameObject;
			openDialog.transform.SetParent(windowCanvas);
			openDialog.transform.SetAsLastSibling();
		}
	}

	/// <summary>
	/// Shows the save dialog
	/// </summary>
	public void SaveLevel() {
		if (saveDialog == null) {
			saveDialog = Instantiate(saveDialogPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f), Quaternion.identity) as GameObject;
			saveDialog.transform.SetParent(windowCanvas);
			saveDialog.transform.SetAsLastSibling();
		}
	}

	/// <summary>
	/// Opens the level settings dialog
	/// </summary>
	public void LevelSettings() {

	}

	/// <summary>
	/// Copys the selected blocks onto the cursor
	/// </summary>
	public void Copy() {
		selectBox.GetComponent<SelectTool>().Copy();
		BlockCursor.offset = Vector3.zero;
	}

	/// <summary>
	/// Toggles the visibility of the grid
	/// </summary>
	public void ToggleGrid() {
		GridRenderer.ToggleGrid();
	}

	/// <summary>
	/// Sets the gamemode to play
	/// </summary>
	public void PlayLevel() {
		GamemodeManager.instance.SetGamemode(Gamemode.Play);
	}

	/// <summary>
	/// Uploads a level to the level browser
	/// </summary>
	public void UploadLevel() {

	}

	/// <summary>
	/// Moves the floor up by 1
	/// </summary>
	public void UpFloor() {
		CameraMove.instance.floor++;
	}

	/// <summary>
	/// Moves the floor down by 1
	/// </summary>
	public void DownFloor() {
		CameraMove.instance.floor--;
	}
}
