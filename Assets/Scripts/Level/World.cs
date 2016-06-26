using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// The current level that is open in the game
/// </summary>
public class World : MonoBehaviour {
	public static World instance;

	/// <summary>
	/// If the game should use chunks
	/// </summary>
	public static bool useChunks = false;

	//Used only if useChunks is false
	Block[,,] blocks = new Block[worldBlockSize, worldBlockSize, worldBlockSize];
	Dictionary<Collider, Block> colliders = new Dictionary<Collider, Block>();

	public Dictionary<WorldPos, Chunk> chunks = new Dictionary<WorldPos, Chunk>();
	public GameObject chunkPrefab;
	const int worldSize = 10;
	public const int worldBlockSize = 100;

	void Start() {
		instance = this;

		if (useChunks) {
			for (int x = 0; x < worldSize; x++) {
				for (int y = 0; y < worldSize; y++) {
					for (int z = 0; z < worldSize; z++) {
						CreateChunk(x * Chunk.chunkSize, y * Chunk.chunkSize, z * Chunk.chunkSize);
					}
				}
			}
		} else {
			ClearWorld();
		}
	}

	void Update() {

	}

	/// <summary>
	/// Saves the level
	/// </summary>
	/// <param name="fileName">File name of the level</param>
	public void Save(string fileName) {
		FileStream file = File.Create(Application.persistentDataPath + "/" + fileName + ".level");
		BinaryWriter writer = new BinaryWriter(file);

		try {
			LevelSerializer.SaveLevel(blocks, writer);
		} catch (System.Exception ex) {
			Debug.LogException(ex);
		}
	}

	/// <summary>
	/// Loads a level
	/// </summary>
	/// <param name="fileName">File name of the level</param>
	public void Load(string fileName) {
		FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + ".level", FileMode.Open);
		BinaryReader reader = new BinaryReader(file);

		try {
			Block[,,] loadedBlocks = LevelSerializer.LoadLevel(reader);

			for (int x = 0; x < worldBlockSize; x++) {
				for (int y = 0; y < worldBlockSize; y++) {
					for (int z = 0; z < worldBlockSize; z++) {
						SetBlock(x, y, z, loadedBlocks[x, y, z]);
					}
				}
			}
		} catch (System.Exception ex) {
			Debug.LogException(ex);
		}
	}

	/// <summary>
	/// Replaces all the blocks with air blocks
	/// </summary>
	public void ClearWorld() {
		if (useChunks) {
			foreach (Chunk c in chunks.Values) {
				c.ClearChunk();
			}
		} else {
			for (int x = 0; x < worldBlockSize; x++) {
				for (int y = 0; y < worldBlockSize; y++) {
					for (int z = 0; z < worldBlockSize; z++) {
						SetBlock(x, y, z, new AirBlock());
					}
				}
			}
		}
	}

	/// <summary>
	/// Creates an empty chunk
	/// </summary>
	/// <param name="x">X position of the chunk</param>
	/// <param name="y">Y position of the chunk</param>
	/// <param name="z">Z position of the chunk</param>
	public void CreateChunk(int x, int y, int z) {
		WorldPos worldPos = new WorldPos(x, y, z);

		//Instantiate the chunk with a 0.5f offset
		GameObject instance = Instantiate(chunkPrefab, new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), Quaternion.identity) as GameObject;
		instance.transform.SetParent(transform);

		Chunk chunk = instance.GetComponent<Chunk>();

		chunk.pos = worldPos;
		chunk.world = this;

		//Add to the chunks dictionary
		chunks.Add(worldPos, chunk);

		chunk.ClearChunk();
	}

	/// <summary>
	/// Destroys and removes a chunk from the world
	/// </summary>
	/// <param name="x">X position of the chunk</param>
	/// <param name="y">Y position of the chunk</param>
	/// <param name="z">Z position of the chunk</param>
	public void DestroyChunk(int x, int y, int z) {
		Chunk chunk = null;
		if (chunks.TryGetValue(new WorldPos(x, y, z), out chunk)) {
			Destroy(chunk.gameObject);
			chunks.Remove(new WorldPos(x, y, z));
		}
	}

	/// <summary>
	/// Get a chunk based on its position
	/// </summary>
	/// <param name="x">X position of the chunk</param>
	/// <param name="y">Y position of the chunk</param>
	/// <param name="z">Z position of the chunk</param>
	/// <returns>The chunk object</returns>
	public Chunk GetChunk(int x, int y, int z) {
		WorldPos pos = new WorldPos();
		float multiple = Chunk.chunkSize;
		pos.x = Mathf.FloorToInt(x / multiple) * Chunk.chunkSize;
		pos.y = Mathf.FloorToInt(y / multiple) * Chunk.chunkSize;
		pos.z = Mathf.FloorToInt(z / multiple) * Chunk.chunkSize;

		Chunk containerChunk = null;

		chunks.TryGetValue(pos, out containerChunk);

		return containerChunk;
	}

	/// <summary>
	/// Gets a block based on its position
	/// </summary>
	/// <param name="x">X position of the block</param>
	/// <param name="y">Y position of the block</param>
	/// <param name="z">Z position of the block</param>
	/// <returns>The block object</returns>
	public Block GetBlock(int x, int y, int z) {
		if (useChunks) {
			Chunk containerChunk = GetChunk(x, y, z);

			if (containerChunk != null) {
				Block block = containerChunk.GetBlock(x - containerChunk.pos.x, y - containerChunk.pos.y, z - containerChunk.pos.z);
				return block;
			} else {
				return new AirBlock();
			}
		} else {
			return blocks[x, y, z];
		}
	}

	/// <summary>
	/// Set a block in the world
	/// </summary>
	/// <param name="x">X position of the block</param>
	/// <param name="y">Y position of the block</param>
	/// <param name="z">Z position of the block</param>
	/// <param name="block">The block to set</param>
	public void SetBlock(int x, int y, int z, Block block) {
		if (useChunks) {
			Chunk chunk = GetChunk(x, y, z);

			if (chunk != null) {
				chunk.SetBlock(x - chunk.pos.x, y - chunk.pos.y, z - chunk.pos.z, block);
				chunk.update = true;
			}
		} else {
			if (InRange(x) && InRange(y) && InRange(z)) {
				if (blocks[x, y, z] != null) {
					blocks[x, y, z].DestroyBlock();
				}

				block.InstantiateBlock(transform, new Vector3(x, y, z) + new Vector3(0.5f, 0.5f, 0.5f), x, y, z, blocks);

				blocks[x, y, z] = block;

				//Create sibling gameobject for collider
				if (!(block is AirBlock)) {
					GameObject colliderGameObject = new GameObject("Selection Collider");
					colliderGameObject.transform.SetParent(block.gameObject.transform, false);
					colliderGameObject.layer = 9;

					//Create collider
					Collider collider = blocks[x, y, z].GetCollider(colliderGameObject, Vector3.zero);
					if (collider != null) {
						colliders.Add(collider, blocks[x, y, z]);
					}
				}

				//Update surrounding blocks
				for (int xx = -1; xx <= 1; xx++) {
					for (int yy = -1; yy <= 1; yy++) {
						for (int zz = -1; zz <= 1; zz++) {
							if (x != 0 && y != 0 && z != 0) {
								if (InRange(x + xx) && InRange(y + yy) && InRange(z + zz)) {
									if (blocks[x + xx, y + yy, z + zz] != null) {
										blocks[x + xx, y + yy, z + zz].UpdateBlock(x + xx, y + yy, z + zz, blocks);
									}
								}
							}
						}
					}
				}
			}
		}
	}

	bool InRange(int index) {
		if (index < 0 || index >= worldBlockSize)
			return false;

		return true;
	}

	public Block GetBlockFromCollider(Collider coll) {
		return colliders[coll];
	}
}
