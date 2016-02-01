using UnityEngine;
using System.Collections;

public class ToolbarManager : MonoBehaviour {
	public GameObject selectBox;
	public World world;

	public void setTool(int t) {
		switch (t) {
			case 0:
				UIManager.tool = Tool.SELECT;
				break;
			case 1:
				UIManager.tool = Tool.BLOCK;
				break;
			case 2:
				UIManager.tool = Tool.PAN;
				break;
			case 3:
				UIManager.tool = Tool.ORBIT;
				break;
			case 4:
				UIManager.tool = Tool.ZOOM;
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
		//load("save1");
	}

	public void saveLevel() {
		/*GameObject g = Instantiate(saveWindow, new Vector3(Screen.width / 2, Screen.height / 2, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
		g.transform.name = "SaveWindow";
		g.transform.SetParent(windowCanvas.transform);*/
	}

	public void levelSettings() {

	}

	public void copy() {
		//selectBox.GetComponent<SelectBox>().copy();
		//setTool(1);
	}

	public void rotateBlock() {

	}

	public void blockProperties() {

	}

	public void playLevel() {
		UIManager.gamemode = Gamemode.PLAY;
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
