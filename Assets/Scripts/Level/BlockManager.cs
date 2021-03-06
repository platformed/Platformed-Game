﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class BlockManager : MonoBehaviour {
	static List<Block> blocks = new List<Block>();
	static List<BlockCategory> blockCategories = new List<BlockCategory>();
	static List<GameObject> blockButtons = new List<GameObject>();
	public GameObject blockButton;
	public GameObject vrBlockButton;
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
		AddBlock(new CarvedStoneBlock(), BlockCategory.Block);
		AddBlock(new CrateBlock(), BlockCategory.Block);
		AddBlock(new DirtBlock(), BlockCategory.Block);
		AddBlock(new SticksBoxBlock(), BlockCategory.Block);
		AddBlock(new SticksOpenBoxBlock(), BlockCategory.Block);

		//Colors
		AddBlock(new ColorBlackBlock(), BlockCategory.Block);
		AddBlock(new ColorGrayBlock(), BlockCategory.Block);
		AddBlock(new ColorWhiteBlock(), BlockCategory.Block);
		AddBlock(new ColorTanBlock(), BlockCategory.Block);
		AddBlock(new ColorLightBrownBlock(), BlockCategory.Block);
		AddBlock(new ColorBrownBlock(), BlockCategory.Block);
		AddBlock(new ColorRedBlock(), BlockCategory.Block);
		AddBlock(new ColorOrangeBlock(), BlockCategory.Block);
		AddBlock(new ColorYellowBlock(), BlockCategory.Block);
		AddBlock(new ColorLightGreenBlock(), BlockCategory.Block);
		AddBlock(new ColorGreenBlock(), BlockCategory.Block);
		AddBlock(new ColorLightBlueBlock(), BlockCategory.Block);
		AddBlock(new ColorBlueBlock(), BlockCategory.Block);
		AddBlock(new ColorTealBlock(), BlockCategory.Block);
		AddBlock(new ColorPurpleBlock(), BlockCategory.Block);
		AddBlock(new ColorPinkBlock(), BlockCategory.Block);

		//Floors
		AddBlock(new GrassBlock(), BlockCategory.Floor);
		AddBlock(new SticksBlock(), BlockCategory.Floor);

		//Props
		AddBlock(new TorchBlock(), BlockCategory.Prop);
		AddBlock(new TallGrassBlock(), BlockCategory.Prop);
		AddBlock(new BoulderBlock(), BlockCategory.Prop);
		AddBlock(new WoodenBridgeBlock(), BlockCategory.Prop);
		AddBlock(new FenceBlock(), BlockCategory.Prop);
		//AddBlock(new FenceCornerBlock(), BlockCategory.Prop);
		//AddBlock(new FenceThreeWayBlock(), BlockCategory.Prop);
		//AddBlock(new FenceFourWayBlock(), BlockCategory.Prop);

		//Hazards
		AddBlock(new FallingSticksBlock(), BlockCategory.Hazard);

		//Collectables
		AddBlock(new GemBlock(), BlockCategory.Collectable);

		//Flags
		AddBlock(new FlagStartBlock(), BlockCategory.Flag);
		AddBlock(new FlagCheckpointBlock(), BlockCategory.Flag);
		AddBlock(new FlagFinishBlock(), BlockCategory.Flag);

		//Unused
		/*
		AddBlock(new GrayBricksBlock(), BlockCategory.Block);
		AddBlock(new PillarBlock(), BlockCategory.Block);
		AddBlock(new LeavesBlock());
		AddBlock(new StoneFloorBlock(), BlockCategory.Floor);
		AddBlock(new TileFloorBlock(), BlockCategory.Floor);
		AddBlock(new BrickStairsBlock(), BlockCategory.Prop);
		AddBlock(new DirtWallBlock(), BlockCategory.Block);
		AddBlock(new BarkBlock(), BlockCategory.Block);
		AddBlock(new WoodBlock(), BlockCategory.Block);
		*/
	}

	/// <summary>
	/// Adds the block buttons to the block library
	/// </summary>
	void AddBlockButton(Block block, BlockCategory category) {
		//Ignore air
		if (block.GetName() != "Air") {
			//Instatiate object
			GameObject button = Instantiate(blockButton) as GameObject;
			button.transform.SetParent(blockLibrary);
			button.name = block.GetDisplayName() + " Button";

			//Set button onClick
			Button b = button.GetComponent<Button>();
			string n = block.GetName();
			b.onClick.AddListener(() => DesignManager.instance.SetToolBlock(n));

			//Set icon
			Image icon = button.transform.GetChild(0).GetComponent<Image>();
			Texture2D texture = Resources.Load("Block Icons/" + block.GetName()) as Texture2D;
			if (texture == null) {
				texture = Resources.Load("Block Icons/Error") as Texture2D;
			}
			icon.overrideSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

			//Add to list
			blockCategories.Add(category);
			blockButtons.Add(button);
		}
	}

	void AddVRBlockButton(Block block, BlockCategory category) {
		//Ignore air
		if (block.GetName() != "Air") {
			//Instatiate object
			GameObject button = Instantiate(vrBlockButton) as GameObject;
			button.transform.SetParent(vrBlockLibrary);
			button.transform.localScale = Vector3.one;//new Vector3(1f / 100f, 1f / 100f, 1f / 100f);
			button.name = block.GetDisplayName() + " Button";

			//Set button onClick
			VRBlockButton b = button.GetComponent<VRBlockButton>();
			b.block = block;

			//Set icon
			Image icon = button.transform.GetChild(0).GetComponent<Image>();
			Texture2D texture = Resources.Load("Block Icons/" + block.GetName()) as Texture2D;
			if (texture == null) {
				texture = Resources.Load("Block Icons/Error") as Texture2D;
			}
			icon.overrideSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

			//Add to list
			blockCategories.Add(category);
			blockButtons.Add(button);
		}
	}

	void AddBlock(Block block, BlockCategory category) {
		blocks.Add(block);
		AddBlockButton(block, category);
		AddVRBlockButton(block, category);
	}

	public static void UpdateCategory(BlockCategory category) {
		for (int i = 0; i < blockButtons.Count; i++) {
			if (blockCategories[i] == category) {
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
		Block b = blocks.Find(x => x.GetName() == name);

		if (b == null) {
			return new AirBlock();
		}

		return b.Copy();
	}
}