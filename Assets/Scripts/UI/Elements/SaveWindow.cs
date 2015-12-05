using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class SaveWindow : MonoBehaviour {
	public InputField inputField;
	public GameObject windowPanel;
	public static GameObject levelButton;

	List<LevelButton> levels = new List<LevelButton>();

	public void save() {
		//TODO
	}

	void updateLevelList() {
		levels.Clear();

		string[] files = Directory.GetFiles(Application.persistentDataPath + "/", "*.level");
		foreach(string s in files) {
			GameObject g = Instantiate(levelButton) as GameObject;
			g.transform.SetParent(windowPanel.transform);
			LevelButton b = g.GetComponent<LevelButton>();
			
			//TODO: save levelname and date in level file
			b.setName(s);
		}
	}

	void Start () {
		levelButton = Resources.Load("UI Elements/LevelButton") as GameObject;
		updateLevelList();
	}
	
	void Update () {
	
	}
}
