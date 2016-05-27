using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CategorySelector : MonoBehaviour {
	public static BlockCategory category = BlockCategory.Block;
	public static bool isVisible = false;

	public Image icon;
	public RectTransform mask;
	public RectTransform menu;
	public RectTransform shadow;
	public GameObject overlay;

	float maskHeight = 52f;
	float menuY = 0f;
	Color iconColor = new Color(1f, 1f, 1f, 97f / 255f);

	void Start() {

	}

	void Update() {
		mask.sizeDelta = new Vector2(mask.sizeDelta.x, Mathf.Lerp(mask.sizeDelta.y, maskHeight, Time.deltaTime * 10f));
		menu.localPosition = new Vector2(menu.localPosition.x, Mathf.Lerp(menu.localPosition.y, menuY, Time.deltaTime * 10f));
		icon.color = Color.Lerp(icon.color, iconColor, Time.deltaTime * 30f);

		//shadow.anchoredPosition = new Vector2(shadow.anchoredPosition.x, -64f - mask.sizeDelta.y);
	}

	public void ShowMenu() {
		maskHeight = 312f;
		
		menu.localPosition = new Vector2(menu.localPosition.x, 52f * (int) category);
		menuY = 0f;

		iconColor = Color.clear;

		overlay.SetActive(true);
		isVisible = true;
	}

	public void HideMenu() {
		maskHeight = 52f;
		
		menu.localPosition = new Vector2(menu.localPosition.x, 0);
		menuY = 52f * (int)category;

		iconColor = new Color(1f, 1f, 1f, 97f / 255f);

		overlay.SetActive(false);
		isVisible = false;
	}

	public void SelectCategory(int i) {
		if (!isVisible) {
			ShowMenu();
			return;
		}

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

	/*string GetCategoryName(BlockCategory c) {
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
	}*/
}

public enum BlockCategory {
	Block,
	Floor,
	Prop,
	Hazard,
	Collectable,
	Flag
}