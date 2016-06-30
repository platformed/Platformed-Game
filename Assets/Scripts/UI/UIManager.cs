using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
	public static UIManager instance;

	[HideInInspector]
	public bool navDrawerEnabled = false;
	[HideInInspector]
	public bool isDragging = false;
	[HideInInspector]
	public bool mouseOverWindow = false;
	[HideInInspector]
	public bool inputFieldSelected = false;

	void Awake() {
		instance = this;
	}

	/// <summary>
	/// Reterns weather the user can interact with the UI
	/// </summary>
	/// <returns>Weather the user can interact with the UI</returns>
	public bool CanInteractUI() {
		if (isDragging) {
			return false;
		}
		if (mouseOverWindow) {
			return false;
		}
		if (navDrawerEnabled) {
			return false;
		}
		if (inputFieldSelected) {
			return false;
		}

		return true;
	}
}