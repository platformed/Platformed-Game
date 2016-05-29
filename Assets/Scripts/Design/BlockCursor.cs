﻿using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A cursor that shows where you are placing blocks
/// </summary>
public class BlockCursor : MonoBehaviour {
	float smoothPos = 18f;
	float smoothRot = 20f;
	public World world;

	static Block[,,] block;
	public static Vector3 offset = Vector3.zero;

	MeshRenderer meshRenderer;
	MeshFilter filter;

	static Transform parent;

	List<string> blockTypes = new List<string>();

	//If the cursor should be updated at the end of the frame
	static bool update = true;

	void Start() {
		block = new Block[,,] { { { new BricksBlock() } } };

		meshRenderer = GetComponent<MeshRenderer>();
		filter = GetComponent<MeshFilter>();

		parent = transform;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.R)) {
			Rotate();
		}

		Vector3 pos = transform.position;

		if (UIManager.tool == Tool.Block && UIManager.CanInteract()) {
			Vector3 hit = UIManager.Raycast();
			pos = new Vector3(Mathf.Floor(hit.x), Mathf.Floor(hit.y), Mathf.Floor(hit.z)) + new Vector3(0.5f, 0.5f, 0.5f);

			bool singleBlock = block.GetLength(0) == 1 && block.GetLength(1) == 1 && block.GetLength(2) == 1;
            if (singleBlock ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0)) {
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

			if (Input.GetMouseButton(1)) {
				world.SetBlock((int)hit.x, (int)hit.y, (int)hit.z, new AirBlock());
			}
		}

		transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * smoothPos);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * smoothRot);

		if (update) {
			RenderCursor();
			update = false;
		}

		CheckVisibility();
	}

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
						b.InstantiateBlock(parent, new Vector3(x, y, z) + new Vector3(0f, -0.5f, 0f), x, y, z, block);
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

		update = true;
	}

	public void Copy(Block[,,] blocks, Vector3 offset) {
		block = blocks;
		BlockCursor.offset = offset;

		update = true;
	}

	void RenderCursor() {
		MeshData data = new MeshData();

		blockTypes.Clear();
		for (int x = 0; x < block.GetLength(0); x++) {
			for (int y = 0; y < block.GetLength(1); y++) {
				for (int z = 0; z < block.GetLength(2); z++) {
					if (block[x, y, z].GetName() != "Air") {
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
			materials[i] = Resources.Load("Blocks/" + blockTypes[i] + "/" + blockTypes[i] + "Material") as Material;
		}
		meshRenderer.materials = materials;
	}

	void CheckVisibility() {
		float size = UIManager.worldSize;

		meshRenderer.enabled = true;

		if (UIManager.tool != Tool.Block) {
			meshRenderer.enabled = false;
		}

		if (transform.position.x < 0) {
			meshRenderer.enabled = false;
		}
		if (transform.position.x > size) {
			meshRenderer.enabled = false;
		}

		if (transform.position.z < 0) {
			meshRenderer.enabled = false;
		}
		if (transform.position.z > size) {
			meshRenderer.enabled = false;
		}
		if (!UIManager.CanInteract()) {
			meshRenderer.enabled = false;
		}
	}
}
