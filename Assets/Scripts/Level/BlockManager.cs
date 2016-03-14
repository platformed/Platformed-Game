using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class BlockManager : MonoBehaviour {
	static List<Block> blocks = new List<Block>();
	public GameObject blockButton;
	public Transform blockLibrary;

	void Awake() {
		AddBlocks();
	}

	void Start() {
		AddBlockButtons();
	}

	void AddBlocks() {
		AddBlock(new AirBlock());
		AddBlock(new BricksBlock());
		AddBlock(new CrateBlock());
		AddBlock(new DirtWallBlock());
		AddBlock(new GrassFloorBlock());
		AddBlock(new StoneFloorBlock());
		AddBlock(new TempleFloor());
		//AddBlock(new BarkBlock());
		//AddBlock(new WoodBlock());
		//AddBlock(new LeavesBlock());
		//AddBlock(new GrassBlock());
	}

	/// <summary>
	/// Adds the block buttons to the block library
	/// </summary>
	void AddBlockButtons() {
		foreach (Block block in blocks) {
			//Ignore air
			if (block.GetName() != "Air") {
				//Instatiate object
				GameObject button = Instantiate(blockButton) as GameObject;
				button.transform.SetParent(blockLibrary);
				button.name = "BlockButton" + block.GetName();

				//Set button onClick
				Button b = button.GetComponent<Button>();
				string n = block.GetName();
				b.onClick.AddListener(() => UIManager.setToolBlock(n));

				//Set text of button
				Text name = button.GetComponentInChildren<Text>();
				name.text = block.GetDisplayName();
			}
		}
	}

	void AddBlock(Block block) {
		blocks.Add(block);
	}

	public static List<Block> GetBlocks() {
		return blocks;
	}

	public static Block GetBlock(string name) {
		return blocks.Find(x => x.GetName() == name);
	}
}
