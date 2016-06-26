using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class SaveDialog : MonoBehaviour {
	public InputField inputField;
	public Transform buttonParent;

	/// <summary>
	/// The file name of the selected level
	/// </summary>
	string fileName;

	void Start() {
		//Get level button prefab
		GameObject levelButtonPrefab = Resources.Load("UI/Buttons/LevelButton") as GameObject;

		//Find level files
		DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
		FileInfo[] info = dir.GetFiles("*.level");

		//Add a button for each level
		foreach (FileInfo i in info) {
			string name = Path.GetFileNameWithoutExtension(i.Name);

			GameObject levelButton = Instantiate(levelButtonPrefab);
			levelButton.transform.SetParent(buttonParent);

			levelButton.GetComponentInChildren<Text>().text = name;
			levelButton.GetComponent<Button>().onClick.AddListener(() => Select(name));
		}
	}

	void Update() {
		if (inputField != null) {
			UIManager.instance.inputFieldSelected = inputField.isFocused;
		}
	}

	/// <summary>
	/// When the input field changes, update the file name
	/// </summary>
	public void InputFieldChange() {
		fileName = inputField.text;
	}

	/// <summary>
	/// Sets the file name and input field text
	/// </summary>
	/// <param name="fileName">The file name of the level</param>
	public void Select(string fileName) {
		this.fileName = fileName;

		if (inputField != null) {
			inputField.text = fileName;
		}
	}

	/// <summary>
	/// Saves the level with the current file name
	/// </summary>
	public void Save() {
		World.instance.Save(fileName);
	}

	/// <summary>
	/// Opens the level with the current file name
	/// </summary>
	public void Open() {
		World.instance.Load(fileName);
	}

	/// <summary>
	/// Opens the folder where the levels are located
	/// </summary>
	public void OpenLevelFolder() {
		string path = Application.persistentDataPath;

        if (Application.platform == RuntimePlatform.WindowsPlayer) {
			OpenWindows(path);
		} else {
			OpenMac(path);
		}
    }

	/// <summary>
	/// Opens file explorer in windows
	/// </summary>
	/// <param name="path">Directory to open to</param>
	void OpenWindows(string path) {
		try {
			System.Diagnostics.Process.Start("explorer.exe", "/root," + path);
		} catch { }
	}

	/// <summary>
	/// Opens finder in mac
	/// </summary>
	/// <param name="path">Directory to open to</param>
	void OpenMac(string path) {
		try {
			System.Diagnostics.Process.Start("open", path);
		} catch { }
	}
}
