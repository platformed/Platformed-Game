using UnityEngine;
using System.Collections;

public class DesignManager : MonoBehaviour {
	public static DesignManager instance;

	public Tool tool = Tool.Block;

	void Awake() {
		instance = this;
	}

	/// <summary>
	/// Sets the tool to block with a specific block
	/// </summary>
	/// <param name="name">Name of block</param>
	public void SetToolBlock(string name) {
		tool = Tool.Block;
		BlockCursor.SetBlock(new Block[,,] { { { BlockManager.GetBlock(name) } } });
		BlockCursor.offset = Vector3.zero;

		VRCursor.SetBlock(new Block[,,] { { { BlockManager.GetBlock(name) } } });
	}

	/// <summary>
	/// Reterns weather the user can interact with the level
	/// </summary>
	/// <returns>Weather the user can interact with the level</returns>
	public bool CanInteractLevel() {
		if (UIManager.instance.isDragging) {
			return false;
		}
		if (Input.mousePosition.y > Screen.height - 64 || Input.mousePosition.x > Screen.width - 200) {
			return false;
		}
		if (UIManager.instance.mouseOverWindow) {
			return false;
		}
		if (UIManager.instance.navDrawerEnabled) {
			return false;
		}
		if (UIManager.instance.inputFieldSelected) {
			return false;
		}
		if (CategorySelector.isVisible) {
			return false;
		}
		if (GamemodeManager.instance.IsTransitioning) {
			return false;
		}

		return true;
	}
}

public enum Tool {
	Select,
	Properties,
	Block,
	Pan,
	Orbit,
	Zoom
}
