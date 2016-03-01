using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Settings {
	List<SettingsItem> items = new List<SettingsItem>();
	Transform itemPanel;

	public Settings(Transform itemPanel) {
		this.itemPanel = itemPanel;
	}

	public void AddItem(SettingsItem item) {
		items.Add(item);
		item.Draw(itemPanel);
	}
}
