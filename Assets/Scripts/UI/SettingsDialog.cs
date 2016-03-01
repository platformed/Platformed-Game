using UnityEngine;
using System.Collections;

public class SettingsDialog : MonoBehaviour {
	public Transform itemPanel;
	Settings general;
	Settings input;
	Settings sound;
	Settings video;

	void Start () {
		general = new Settings(itemPanel);
		input = new Settings(itemPanel);
		sound = new Settings(itemPanel);
		video = new Settings(itemPanel);

		general.AddItem(new SliderSettingsItem("Test Slider"));
	}
	
	void Update () {
	
	}
}
