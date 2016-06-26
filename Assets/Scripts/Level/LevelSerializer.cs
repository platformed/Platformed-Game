using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class LevelSerializer {
	/// <summary>
	/// Saves a block array
	/// </summary>
	/// <param name="blocks">Block array to save</param>
	/// <param name="writer">BinaryWriter to write with</param>
	public static void SaveLevel(Block[,,] blocks, BinaryWriter writer) {
		//Write version number
		writer.Write(0);

		//Write block ids
		List<Block> blockList = BlockManager.GetBlocks();

        writer.Write((short)blockList.Count);

		foreach(Block b in blockList) {
			writer.Write(b.GetName());
		}

		//Write the number of blocks
		int blockCount = 0;
		for (int x = 0; x < World.worldBlockSize; x++) {
			for (int y = 0; y < World.worldBlockSize; y++) {
				for (int z = 0; z < World.worldBlockSize; z++) {
					if (!(blocks[x, y, z] is AirBlock)) {
						blockCount++;
					}
				}
			}
		}
		writer.Write(blockCount);

		//Write the blocks
		for (int x = 0; x < World.worldBlockSize; x++) {
			for (int y = 0; y < World.worldBlockSize; y++) {
				for (int z = 0; z < World.worldBlockSize; z++) {
					if (!(blocks[x, y, z] is AirBlock)) {
						writer.Write((byte)x);
						writer.Write((byte)y);
						writer.Write((byte)z); 
						writer.Write(blocks[x, y, z].rotation);
						writer.Write((short)blockList.FindIndex(i => i.GetName() == blocks[x, y, z].GetName()));
					}
				}
			}
		}

		writer.Close();
	}

	/// <summary>
	/// Reads a level to a block array
	/// </summary>
	/// <param name="reader">BinaryReader to read with</param>
	/// <returns>Block array of level</returns>
	public static Block[,,] LoadLevel(BinaryReader reader) {
		//Read version number
		reader.ReadInt32();

		//Read block ids
		List<Block> blockList = new List<Block>();

		short blockListCount = reader.ReadInt16();

		for (short i = 0; i < blockListCount; i++) {
			blockList.Add(BlockManager.GetBlock(reader.ReadString()));
		}

		//Read number of blocks
		int blockCount = reader.ReadInt32();

		//Read blocks
		Block[,,] blocks = new Block[World.worldBlockSize, World.worldBlockSize, World.worldBlockSize];
		for (int x = 0; x < World.worldBlockSize; x++) {
			for (int y = 0; y < World.worldBlockSize; y++) {
				for (int z = 0; z < World.worldBlockSize; z++) {
					blocks[x, y, z] = new AirBlock();
				}
			}
		}

		for (int i = 0; i < blockCount; i++) {
			byte x = reader.ReadByte();
			byte y = reader.ReadByte();
			byte z = reader.ReadByte();
			byte rotation = reader.ReadByte();
			short blockId = reader.ReadInt16();

			blocks[x, y, z] = blockList[blockId].Copy();
			blocks[x, y, z].rotation = rotation;
		}

		reader.Close();
		return blocks;
	}
}
