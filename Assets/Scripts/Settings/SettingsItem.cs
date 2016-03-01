using UnityEngine;
using System.Collections;

public abstract class SettingsItem {
	string label;

	public SettingsItem(string label) {
		this.label = label;
	}

	public abstract void Draw(Transform parent);

	public string GetLabel() {
		return label;
	}
}
