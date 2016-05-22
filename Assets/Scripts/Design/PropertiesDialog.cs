using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PropertiesDialog : MonoBehaviour {
	public Text title;

	public void SetTitle(string title) {
		this.title.text = title + " Properties";
	}

	public void SetProperties(List<BlockProperty> properties) {
		foreach(BlockProperty p in properties){

		}
	}
}
