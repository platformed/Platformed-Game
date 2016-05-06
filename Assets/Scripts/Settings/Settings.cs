using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Settings {
	List<SettingsItem> settings = new List<SettingsItem>();
	Transform itemPanel;

	public Settings(Transform itemPanel) {
		this.itemPanel = itemPanel;
	}

	public void AddItem(SettingsItem item) {
		settings.Add(item);
		item.Draw(itemPanel);
	}
}
