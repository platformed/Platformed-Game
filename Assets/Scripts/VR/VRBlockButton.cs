using UnityEngine;
using System.Collections;

public class VRBlockButton : MonoBehaviour {
	public Block block;

	public void Click() {
		UIManager.SetToolBlock(block.GetName());
	}
}
