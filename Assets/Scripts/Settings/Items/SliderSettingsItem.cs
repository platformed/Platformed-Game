using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderSettingsItem : SettingsItem {
	public SliderSettingsItem(string label) : base(label) {

	}

	public override void Draw(Transform parent) {
		GameObject instace = Object.Instantiate(Resources.Load("UI/Settings/Slider") as GameObject);
		instace.transform.SetParent(parent);

		instace.GetComponentInChildren<Text>().text = GetLabel();
	}
}
