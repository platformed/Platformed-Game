using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A cursor that shows where you are placing blocks
/// </summary>
public class Cursor : MonoBehaviour {
	Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);
	float smoothPos = 30f;
	float smoothRot = 20f;
	public World world;

	public static Block[,,] block;
	byte rotation;

	new MeshRenderer renderer;
	MeshFilter filter;

	List<string> blockTypes = new List<string>();

	void Start() {
		block = new Block[,,] { { { new BricksBlock() } } };
		renderer = GetComponent<MeshRenderer>();
		filter = GetComponent<MeshFilter>();
	}

	void Update() {
		RenderCursor();

		if (Input.GetKeyDown(KeyCode.R)) {
			Rotate();
		}

		Vector3 pos = transform.position;

		if (UIManager.tool == Tool.BLOCK && UIManager.canInteract()) {
			Vector3 hit = UIManager.raycast();
			pos = new Vector3(Mathf.Floor(hit.x), Mathf.Floor(hit.y), Mathf.Floor(hit.z)) + offset;

			if (Input.GetMouseButton(0)) {
				//block.SetRotation(rotation);
				//world.SetBlock((int)hit.x, (int)hit.y, (int)hit.z, block[0, 0, 0].Copy());
				//block.SetRotation(0);
				for (int x = 0; x < block.GetLength(0); x++) {
					for (int y = 0; y < block.GetLength(1); y++) {
						for (int z = 0; z < block.GetLength(2); z++) {
							if (block[x, y, z].GetName() != "Air") {
								world.SetBlock((int)hit.x + x, (int)hit.y + y, (int)hit.z + z, block[x, y, z].Copy());
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
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90 * rotation, 0), Time.deltaTime * smoothRot);
		clampPos();
	}

	public void Rotate() {
		Block[,,] rotatedBlocks = new Block[block.GetLength(2), block.GetLength(1), block.GetLength(0)];

		//Transpose
		for (int x = 0; x < block.GetLength(0); x++) {
			for (int y = 0; y < block.GetLength(1); y++) {
				for (int z = 0; z < block.GetLength(2); z++) {
					rotatedBlocks[z, y, x] = block[block.GetLength(0) - x - 1, y, z];
				}
			}
		}

		

		//Flip
		/*for (int x = 0; x < rotatedBlocks.GetLength(0); x++) {
			for (int y = 0; y < rotatedBlocks.GetLength(1); y++) {
				Array.Reverse(rotatedBlocks);
			}
		}*/

		block = rotatedBlocks;
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

						data = block[x, y, z].BlockData(null, x, y, z, data, submesh, true);
					}
				}
			}
		}

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
		renderer.materials = materials;
	}

	void clampPos() {
		float size = UIManager.worldSize;

		renderer.enabled = true;

		if (transform.position.x < 0) {
			renderer.enabled = false;
			//transform.position = new Vector3(0.5f, transform.position.y, transform.position.z);
		}
		if (transform.position.x > size) {
			renderer.enabled = false;
			//transform.position = new Vector3(size, transform.position.y, transform.position.z);
		}

		if (transform.position.z < 0) {
			renderer.enabled = false;
			//transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);
		}
		if (transform.position.z > size) {
			renderer.enabled = false;
			//transform.position = new Vector3(transform.position.x, transform.position.y, size);
		}
		if (!UIManager.canInteract()) {
			renderer.enabled = false;
		}
	}
}
