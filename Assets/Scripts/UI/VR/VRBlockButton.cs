using UnityEngine;
using System.Collections;

public class VRBlockButton : MonoBehaviour {
	public Block block;
	BoxCollider clickCollider;

	void Start() {
		clickCollider = GetComponent<BoxCollider>();
	}

	public void Click() {
		UIManager.setToolBlock(block.GetName());
		Debug.Log("clicked " + block.GetName());
	}
}
