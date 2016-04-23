using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

/// <summary>
/// Represents a 10 by 10 by 10 area in the level
/// </summary>
public class Chunk : MonoBehaviour {
	MeshFilter filter;
	MeshCollider coll;
	MeshRenderer meshRenderer;

	Block[,,] blocks = new Block[chunkSize, chunkSize, chunkSize];
	public static readonly int chunkSize = 10;

	public World world;
	public WorldPos pos;

	//If this chunk be updated at the end of the frame
	public bool update = true;

	List<string> blockTypes = new List<string>();

	void Start() {
		filter = GetComponent<MeshFilter>();
		coll = GetComponent<MeshCollider>();
		meshRenderer = GetComponent<MeshRenderer>();
	}

	void Update() {
		if (update) {
			update = false;
			UpdateChunk();
		}
	}

	/// <summary>
	/// Replaces all the blocks in the chunk with air blocks
	/// </summary>
	public void ClearChunk() {
		for (int x = 0; x < chunkSize; x++) {
			for (int y = 0; y < chunkSize; y++) {
				for (int z = 0; z < chunkSize; z++) {
					SetBlock(x, y, z, new AirBlock());
				}
			}
		}

		update = true;
	}

	/// <summary>
	/// Gets a block based on its position
	/// </summary>
	/// <param name="x">X position of the block</param>
	/// <param name="y">Y position of the block</param>
	/// <param name="z">Z position of the block</param>
	/// <returns>The block object</returns>
	public Block GetBlock(int x, int y, int z) {
		if (InRange(x) && InRange(y) && InRange(z))
			return blocks[x, y, z];

		return new AirBlock();
	}

	public void SetBlock(int x, int y, int z, Block block) {
		if (InRange(x) && InRange(y) && InRange(z)) {
			blocks[x, y, z] = block;
		} else {
			world.SetBlock(pos.x + x, pos.y + y, pos.z + z, block);
		}
	}

	public static bool InRange(int index) {
		if (index < 0 || index >= chunkSize)
			return false;

		return true;
	}

	/// <summary>
	/// Updates the chunk based on its contents
	/// </summary>
	void UpdateChunk() {
		MeshData data = new MeshData();

		blockTypes.Clear();
		for (int x = 0; x < chunkSize; x++) {
			for (int y = 0; y < chunkSize; y++) {
				for (int z = 0; z < chunkSize; z++) {
					if (blocks[x, y, z].GetName() != "Air") {
						//Try to find if the block type already exists in chunk
						int submesh = blockTypes.IndexOf(blocks[x, y, z].GetName());

						//If there are no blocks with its type, add it
						if (submesh == -1) {
							blockTypes.Add(blocks[x, y, z].GetName());
							submesh = blockTypes.Count - 1;
						}

						data = blocks[x, y, z].BlockData(x, y, z, data, submesh, blocks);
					}
				}
			}
		}

		RenderMesh(data);
	}

	/// <summary>
	/// Sends the mesh information to the mesh and collision components
	/// </summary>
	void RenderMesh(MeshData data) {
		//Update mesh
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

		//Visualize normals
		/*for (int i = 0; i < filter.mesh.vertexCount; i++) {
			Debug.DrawRay(filter.mesh.vertices[i] + pos.ToVector3() + new Vector3(0.5f, 0.5f, 0.5f), filter.mesh.normals[i], Color.yellow, 10);
		}*/

		//Update collision mesh
		coll.sharedMesh = null;
		Mesh mesh = new Mesh();
		mesh.vertices = data.colVerticies.ToArray();
		mesh.triangles = data.colTriangles.ToArray();
		mesh.RecalculateNormals();
		coll.sharedMesh = mesh;
	}
}
