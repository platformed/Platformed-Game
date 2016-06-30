using UnityEngine;
using System.Collections;

public class VRBlockButton : MonoBehaviour {
	public Block block;

	public void Click() {
		DesignManager.instance.SetToolBlock(block.GetName());
	}
}
