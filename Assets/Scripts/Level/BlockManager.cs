using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class BlockManager : MonoBehaviour {
	static List<Block> blocks = new List<Block>();
	static List<BlockCategory> blockCategories = new List<BlockCategory>();
	static List<GameObject> blockButtons = new List<GameObject>();
	public GameObject blockButton;
	public Transform blockLibrary;
	public Transform vrBlockLibrary;

	public BlockIconManager blockIconManager;

	void Start() {
		AddBlocks();
		UpdateCategory(CategorySelector.category);
	}

	void AddBlocks() {
		blocks.Clear();
		blockCategories.Clear();
		blockButtons.Clear();

		//Blocks
		AddBlock(new AirBlock(), BlockCategory.Block);
		AddBlock(new BricksBlock(), BlockCategory.Block);
		AddBlock(new GrayBricksBlock(), BlockCategory.Block);
		AddBlock(new CarvedStoneBlock(), BlockCategory.Block);
		AddBlock(new CrateBlock(), BlockCategory.Block);
		AddBlock(new DirtWallBlock(), BlockCategory.Block);

		//Floors
		AddBlock(new GrassFloorBlock(), BlockCategory.Floor);
		AddBlock(new StoneFloorBlock(), BlockCategory.Floor);
		AddBlock(new TileFloorBlock(), BlockCategory.Floor);

		//Props
		AddBlock(new BoulderBlock(), BlockCategory.Prop);
		AddBlock(new BrickStairsBlock(), BlockCategory.Prop);

		//Unused
		//AddBlock(new PillarBlock(), BlockCategory.Block);
		//AddBlock(new BarkBlock());
		//AddBlock(new WoodBlock());
		//AddBlock(new LeavesBlock());
		//AddBlock(new GrassBlock());
	}

	/// <summary>
	/// Adds the block buttons to the block library
	/// </summary>
	void AddBlockButton(Block block, BlockCategory category, Transform parent, Vector3 scale) {
		//Ignore air
		if (block.GetName() != "Air") {
			//Instatiate object
			GameObject button = Instantiate(blockButton) as GameObject;
			button.transform.position = Vector3.zero;
			button.transform.localScale = scale;
			button.transform.SetParent(parent);
			button.name = block.GetDisplayName() + " Button";

			//Set button onClick
			Button b = button.GetComponent<Button>();
			string n = block.GetName();
			b.onClick.AddListener(() => UIManager.setToolBlock(n));

			//Set text of button
			Image icon = button.transform.GetChild(0).GetComponent<Image>();
			Texture2D texture = Resources.Load("Block Icons/" + block.GetName()) as Texture2D;
            icon.overrideSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

			//Add to list
			blockCategories.Add(category);
			blockButtons.Add(button);
		}
	}

	void AddBlock(Block block, BlockCategory category) {
		blocks.Add(block);
		AddBlockButton(block, category, blockLibrary, Vector3.one);
		AddBlockButton(block, category, vrBlockLibrary, new Vector3(1f / 100f, 1f / 100f, 1f / 100f));
	}

	public static void UpdateCategory(BlockCategory category) {
		for (int i = 0; i < blockButtons.Count; i++) {
			if(blockCategories[i] == category) {
				blockButtons[i].SetActive(true);
			} else {
				blockButtons[i].SetActive(false);
			}
		}
	}

	public static List<Block> GetBlocks() {
		return blocks;
	}

	public static Block GetBlock(string name) {
		return blocks.Find(x => x.GetName() == name).Copy();
	}
}