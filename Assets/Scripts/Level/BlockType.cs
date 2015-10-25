using UnityEngine;
using System.Collections;

public class BlockType {
	protected string name;
	protected string displayName;
	protected bool isCube;
	int id;

	public BlockType() {

	}

	public bool hasModel() {
		return isCube;
	}

	public string getName() {
		return name;
	}

	public string getDisplayName() {
		return displayName;
	}

	public void setID(int i) {
		id = i;
	}

	public int getID() {
		return id;
	}
}