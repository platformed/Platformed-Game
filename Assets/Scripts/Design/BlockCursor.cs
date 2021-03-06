﻿using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A cursor that shows where you are placing blocks
/// </summary>
public class BlockCursor : MonoBehaviour {
	public Transform spawnableBlockRotation;
	byte rotation;

	float smoothPos = 18f;
	float smoothRot = 20f;
	public World world;

	static Block[,,] block;
	public static Vector3 offset = Vector3.zero;

	MeshRenderer meshRenderer;
	MeshFilter filter;

	static Transform spawnableBlockParent;

	List<string> blockTypes = new List<string>();

	//If the cursor should be updated at the end of the frame
	static bool update = true;

	void Start() {
		block = new Block[,,] { { { new BricksBlock() } } };

		meshRenderer = GetComponent<MeshRenderer>();
		filter = GetComponent<MeshFilter>();

		spawnableBlockParent = spawnableBlockRotation;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.R)) {
			Rotate();
		}

		Vector3 pos = transform.position;

		if (DesignManager.instance.tool == Tool.Block && DesignManager.instance.CanInteractLevel()) {
			//Raycast the position of the mouse
			Vector3 hit = CameraOrbit.instance.Raycast();
			pos = new Vector3(Mathf.Floor(hit.x), Mathf.Floor(hit.y), Mathf.Floor(hit.z)) + new Vector3(0.5f, 0.5f, 0.5f);

			//Allow mouse to be held down if it is only a single block
			bool singleBlock = block.GetLength(0) == 1 && block.GetLength(1) == 1 && block.GetLength(2) == 1;
            if (singleBlock ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0)) {
				//Set blocks
				for (int x = 0; x < block.GetLength(0); x++) {
					for (int y = 0; y < block.GetLength(1); y++) {
						for (int z = 0; z < block.GetLength(2); z++) {
							if (block[x, y, z].GetName() != "Air") {
								world.SetBlock((int)(hit.x + x - offset.x), (int)(hit.y + y - offset.y), (int)(hit.z + z - offset.z), block[x, y, z].Copy());
							}
						}
					}
				}
			}

			//Erase with right mouse button
			if (Input.GetMouseButton(1)) {
				world.SetBlock((int)hit.x, (int)hit.y, (int)hit.z, new AirBlock());
			}
		}

		//Set position and rotation of cursor
		transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * smoothPos);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * smoothRot);
		spawnableBlockRotation.rotation = Quaternion.Lerp(spawnableBlockRotation.rotation, Quaternion.Euler(0, 90f * rotation, 0), Time.deltaTime * smoothRot);

		if (update) {
			RenderCursor();
			update = false;
		}

		CheckVisibility();
	}

	/// <summary>
	/// Sets the block of the cursor
	/// </summary>
	/// <param name="newBlock">Block(s) to set to</param>
	public static void SetBlock(Block[,,] newBlock) {
		for (int x = 0; x < block.GetLength(0); x++) {
			for (int y = 0; y < block.GetLength(1); y++) {
				for (int z = 0; z < block.GetLength(2); z++) {
					//Destroy old block if it is spawnable
					if (block[x, y, z] is SpawnableBlock) {
						SpawnableBlock b = (SpawnableBlock)block[x, y, z];
						b.DestroyBlock();
					}
				}
			}
		}

		block = newBlock;

		for (int x = 0; x < block.GetLength(0); x++) {
			for (int y = 0; y < block.GetLength(1); y++) {
				for (int z = 0; z < block.GetLength(2); z++) {
					//Instantiate new block if it is spawnable
					if (newBlock[x, y, z] is SpawnableBlock) {
						SpawnableBlock b = (SpawnableBlock)newBlock[x, y, z];
						b.InstantiateBlock(spawnableBlockParent, new Vector3(x, y, z) - offset, x, y, z, block);
					}
				}
			}
		}

		update = true;
	}

	public void Rotate() {
		Block[,,] rotatedBlocks = new Block[block.GetLength(2), block.GetLength(1), block.GetLength(0)];

		//Rotate
		for (int x = 0; x < block.GetLength(0); x++) {
			for (int y = 0; y < block.GetLength(1); y++) {
				for (int z = 0; z < block.GetLength(2); z++) {
					rotatedBlocks[z, y, x] = block[block.GetLength(0) - x - 1, y, z];
					rotatedBlocks[z, y, x].Rotate(1);
				}
			}
		}

		//Fix offset
		offset = new Vector3(offset.z, offset.y, block.GetLength(0) - offset.x - 1);
		transform.Rotate(0, -90, 0);

		block = rotatedBlocks;

		rotation++;
		rotation %= 4;

		update = true;
	}

	public void Copy(Block[,,] blocks, Vector3 offset) {
		BlockCursor.offset = offset;
		SetBlock(blocks);

		update = true;
	}

	void RenderCursor() {
		MeshData data = new MeshData();

		blockTypes.Clear();
		for (int x = 0; x < block.GetLength(0); x++) {
			for (int y = 0; y < block.GetLength(1); y++) {
				for (int z = 0; z < block.GetLength(2); z++) {
					if (block[x, y, z].GetName() != "Air" && !(block[x, y, z] is SpawnableBlock)) {
						//Try to find if the block type already exists in chunk
						int submesh = blockTypes.IndexOf(block[x, y, z].GetName());

						//If there are no blocks with its type, add it
						if (submesh == -1) {
							blockTypes.Add(block[x, y, z].GetName());
							submesh = blockTypes.Count - 1;
						}

						data = block[x, y, z].BlockData(x, y, z, data, submesh, block);
					}
				}
			}
		}
		data.Offset(-offset);

		//Expand to prevent z-fighting
		data.Expand(0.001f);

		//Clear mesh
		filter.mesh.Clear();

		//Verticies
		filter.mesh.vertices = data.vertices.ToArray();

		//Submeshes
		filter.mesh.subMeshCount = data.triangles.Count;
		for (int i = 0; i < data.triangles.Count; i++) {
			filter.mesh.SetTriangles(data.triangles[i], i);
		}

		//UVs and normals
		filter.mesh.uv = data.uvs.ToArray();
		filter.mesh.normals = data.normals.ToArray();

		//Materials
		Material[] materials = new Material[filter.mesh.subMeshCount];
		for (int i = 0; i < materials.Length; i++) {
			materials[i] = BlockManager.GetBlock(blockTypes[i]).GetMaterial();
		}
		meshRenderer.materials = materials;
	}

	void CheckVisibility() {
		float size = World.worldBlockSize;

		bool visible = true;

		if (DesignManager.instance.tool != Tool.Block) {
			visible = false;
        }

		if (transform.position.x < 0) {
			visible = false;
		}
		if (transform.position.x > size) {
			visible = false;
		}
		if (transform.position.z < 0) {
			visible = false;
		}
		if (transform.position.z > size) {
			visible = false;
		}

		if (!DesignManager.instance.CanInteractLevel()) {
			visible = false;
		}

		meshRenderer.enabled = visible;
		foreach (Transform child in transform) {
			child.gameObject.SetActive(visible);
		}
	}
}
