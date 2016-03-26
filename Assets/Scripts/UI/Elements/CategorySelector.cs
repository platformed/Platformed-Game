using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CategorySelector : MonoBehaviour {
	public static BlockCategory category = BlockCategory.Block;
	public static bool isVisible = false;

	public Text categoryText;
	public GameObject menu;
	public GameObject overlay;

	void Start() {

	}

	void Update() {
		categoryText.text = GetCategoryName(category);
	}

	public void ShowMenu() {
		menu.SetActive(true);
		overlay.SetActive(true);
		isVisible = true;
	}

	public void HideMenu() {
		menu.SetActive(false);
		overlay.SetActive(false);
		isVisible = false;
	}

	public void SelectCategory(int i) {
		switch (i) {
			case 0:
				category = BlockCategory.Block;
				break;
			case 1:
				category = BlockCategory.Floor;
				break;
			case 2:
				category = BlockCategory.Prop;
				break;
			case 3:
				category = BlockCategory.Hazard;
				break;
			case 4:
				category = BlockCategory.Collectable;
				break;
			case 5:
				category = BlockCategory.Flag;
				break;
		}

		BlockManager.UpdateCategory(category);
		HideMenu();
	}

	string GetCategoryName(BlockCategory c) {
		switch (c) {
			case BlockCategory.Block:
				return "Blocks";
			case BlockCategory.Floor:
				return "Floors";
			case BlockCategory.Prop:
				return "Props";
			case BlockCategory.Hazard:
				return "Enemies";
			case BlockCategory.Collectable:
				return "Collectables";
			case BlockCategory.Flag:
				return "Flags";
		}

		return null;
	}
}

public enum BlockCategory {
	Block,
	Floor,
	Prop,
	Hazard,
	Collectable,
	Flag
}