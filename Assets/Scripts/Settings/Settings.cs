using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Settings {
	List<SettingsItem> items = new List<SettingsItem>();

	public Settings() {

	}

	public void AddItem(SettingsItem item) {
		items.Add(item);
	}
}
