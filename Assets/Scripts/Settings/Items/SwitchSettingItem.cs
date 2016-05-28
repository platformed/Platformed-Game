using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchSettingsItem : SettingsItem {
	public SwitchSettingsItem(string label) : base(label) {
		
	}

	public override void Draw(Transform parent) {
		GameObject instace = Object.Instantiate(Resources.Load("UI/Settings/Switch") as GameObject);
		instace.transform.SetParent(parent);

		instace.GetComponentInChildren<Text>().text = GetLabel();
	}
}
