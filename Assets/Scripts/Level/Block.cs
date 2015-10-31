using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Block {
	Vector3 rotation;
	BlockType blockType;

	//static string[] blockNames;
	//static List<string> blockNames = new List<string>();
	static List<BlockType> blocks = new List<BlockType>();

	public Block(BlockType b) {
		blockType = b;
	}

	public static void addBlock(BlockType b) {
		blocks.Add(b);
		//blockNames.Add(b.getName());
	}

	//Updates blockNames
	/*public static void addBlockNames() {
		blockNames = new string[blocks.Count];
		for(int i = 0; i < blocks.Count; i++) {
			blockNames[i] = blocks[i].getName();
		}
	}*/

	public static BlockType getBlock(string name) {
		return blocks.Find(i => i.getName() == name);
	}

	/*public static BlockType getBlock(string name) {
		foreach(BlockType b in blocks) {
			if (b.getName().Equals(name)) {
				return b;
			}
		}
		return null;
	}*/

	public static List<BlockType> getBlocks() {
		return blocks;
	}

	public static Block newBlock(string name) {
		return new Block(getBlock(name));
	}

	public static Block newBlock(BlockType b) {
		return new Block(b);
	}

	public BlockType getBlockType() {
		return blockType;
	}

	public MeshData draw(Chunk chunk, int x, int y, int z, bool cursor){
		if(getBlockType().getName().Equals("Air")){
			return new MeshData();
		}

		Vector3[] v = new Vector3[]{
			new Vector3(0, 0, 0),	//0
			new Vector3(0, 0, 1),	//1
			new Vector3(0, 1, 0),	//2
			new Vector3(0, 1, 1),	//3
			new Vector3(1, 0, 0),	//4
			new Vector3(1, 0, 1),	//5
			new Vector3(1, 1, 0),	//6
			new Vector3(1, 1, 1)	//7
		};

		int[] t = new int[]{0, 2, 1, 3, 1, 2};
		
		Rect u = TextureManager.uvs[blocks.IndexOf(blockType)];

		float cWidth = 1f / TextureManager.atlasWidth;
		float cHeight = 1f / TextureManager.atlasHeight;

		Vector2[] uvs = new Vector2[]{
			new Vector2(u.xMin + cWidth, u.yMax - cHeight),
			new Vector2(u.xMin + cWidth, u.yMin + cHeight),
			new Vector2(u.xMax - cWidth, u.yMax - cHeight),
			new Vector2(u.xMax - cWidth, u.yMin + cHeight)
		};

		MeshData data = new MeshData();

		if (cursor || !chunk.getBlock(x, y - 1, z).getBlockType().hasModel()){
			Face bottom = new Face (new Vector3[]{v[0], v[1], v[4], v[5]}, t, uvs);
			data.add (bottom.getMeshData());
		}
		
		if (cursor || !chunk.getBlock(x, y, z + 1).getBlockType().hasModel()) {
			Face right = new Face (new Vector3[]{v[7], v[5], v[3], v[1]}, t, uvs);
			data.add (right.getMeshData());
		}
		
		if (cursor || !chunk.getBlock (x, y, z - 1).getBlockType().hasModel()) {
			Face left = new Face (new Vector3[]{v [2], v [0], v [6], v [4]}, t, uvs);
			data.add (left.getMeshData ());
		}
		
		if (cursor || !chunk.getBlock (x + 1, y, z).getBlockType().hasModel()) {
			Face back = new Face (new Vector3[]{v [6], v [4], v [7], v [5]}, t, uvs);
			data.add (back.getMeshData ());
		}
		
		if (cursor || !chunk.getBlock (x - 1, y, z).getBlockType().hasModel()) {
			Face front = new Face (new Vector3[]{v [3], v [1], v [2], v [0]}, t, uvs);
			data.add (front.getMeshData ());
		}
		
		if (cursor || !chunk.getBlock (x, y + 1, z).getBlockType().hasModel()) {
			Face top = new Face (new Vector3[]{v [6], v [7], v [2], v [3]}, t, uvs);
			data.add (top.getMeshData ());
		}

		return data;
	}

	public static BlockType getBlockByID(int id) {
		foreach (BlockType b in blocks) {
			if (b.getID() == id) {
				return b;
			}
		}
		return null;
	}

	//TODO: only save block ids that are in the level
	public static void saveBlockIDs(FileStream stream) {
		BinaryFormatter formatter = new BinaryFormatter();

		//TODO: try unsigned int
		int i = 0;
		foreach (BlockType b in blocks) {
			formatter.Serialize(stream, b.getName());
			formatter.Serialize(stream, i);
			i++;
		}
	}

	public static void LoadBlockIDs(FileStream stream) {
		BinaryFormatter formatter = new BinaryFormatter();

		for (int i = 0; i < blocks.Count; i++) {
			string name = Convert.ToString(formatter.Deserialize(stream));
			int id = Convert.ToInt32(formatter.Deserialize(stream));
			BlockType b = getBlock(name);
			b.setID(id);
        }
	}
}
