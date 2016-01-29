using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {
	public Dictionary<WorldPos, Chunk> chunks = new Dictionary<WorldPos, Chunk>();
	public GameObject chunkPrefab;

	void Start() {
		for (int x = -2; x < 2; x++) {
			for (int y = -1; y < 1; y++) {
				for (int z = -1; z < 1; z++) {
					CreateChunk(x * 16, y * 16, z * 16);
				}
			}
		}
	}

	void Update() {

	}

	/// <summary>
	/// Creates an empty chunk
	/// </summary>
	/// <param name="x">X position of the chunk</param>
	/// <param name="y">Y position of the chunk</param>
	/// <param name="z">Z position of the chunk</param>
	public void CreateChunk(int x, int y, int z) {
		WorldPos worldPos = new WorldPos(x, y, z);

		//Instantiate the chunk
		GameObject instance = Instantiate(chunkPrefab, new Vector3(x, y, z), Quaternion.identity) as GameObject;
		instance.transform.SetParent(transform);

		Chunk chunk = instance.GetComponent<Chunk>();

		chunk.pos = worldPos;
		chunk.world = this;

		//Add to the chunks dictionary
		chunks.Add(worldPos, chunk);

		//Test
		for (int xi = 0; xi < 16; xi++) {
			for (int yi = 0; yi < 16; yi++) {
				for (int zi = 0; zi < 16; zi++) {
					if (yi <= 7) {
						SetBlock(x + xi, y + yi, z + zi, new Block());
					} else {
						SetBlock(x + xi, y + yi, z + zi, new BlockAir());
					}
				}
			}
		}
	}

	/// <summary>
	/// Destroys and removes a chunk from the world
	/// </summary>
	/// <param name="x">X position of the chunk</param>
	/// <param name="y">Y position of the chunk</param>
	/// <param name="z">Z position of the chunk</param>
	public void DestroyChunk(int x, int y, int z) {
		Chunk chunk = null;
		if(chunks.TryGetValue(new WorldPos(x, y, z), out chunk)) {
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
		Chunk containerChunk = GetChunk(x, y, z);

		if (containerChunk != null) {
			Block block = containerChunk.GetBlock(x - containerChunk.pos.x, y - containerChunk.pos.y, z - containerChunk.pos.z);
			return block;
		} else {
			return new BlockAir();
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
		Chunk chunk = GetChunk(x, y, z);

		if (chunk != null) {
			chunk.SetBlock(x - chunk.pos.x, y - chunk.pos.y, z - chunk.pos.z, block);
			chunk.update = true;
		}
	}
}
