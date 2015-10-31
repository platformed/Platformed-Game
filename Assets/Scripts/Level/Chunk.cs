using UnityEngine;
using System.IO;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour {
	Block[,,] blocks;
	public static int chunkSize = 10;
	public static int chunkHeight = 100;

	float parts = 20f;
	float div = 0.075f;
	float parts2 = 2.25f;
	float div2 = 0.8f;
	float add = -4f;

	void Start() {
		generateChunk();
		drawChunk();
	}

	//TODO saving and loading
	public void save(FileStream stream) {
		//BinaryFormatter formatter = new BinaryFormatter();
		BinaryWriter writer = new BinaryWriter(stream);

		for (int x = 0; x < chunkSize; x++) {
			for (int z = 0; z < chunkSize; z++) {
				for (int y = 0; y < chunkHeight; y++) {
					//formatter.Serialize(stream, blocks[x, y, z].getBlockType().getID());
					writer.Write(blocks[x, y, z].getBlockType().getID());
				}
			}
		}
	}

	public void load(FileStream stream) {
		//BinaryFormatter formatter = new BinaryFormatter();
		BinaryReader reader = new BinaryReader(stream);

		for (int x = 0; x < chunkSize; x++) {
			for (int z = 0; z < chunkSize; z++) {
				for (int y = 0; y < chunkHeight; y++) {
					//blocks[x, y, z] = Block.newBlock(Block.getBlockByID(Convert.ToInt32(formatter.Deserialize(stream))));
					Block b = Block.newBlock(Block.getBlockByID(reader.ReadInt32()));
					blocks[x, y, z] = b;
				}
			}
		}

		drawChunk();
	}

	public void drawChunk() {
		Mesh mesh = new Mesh();

		MeshData d = new MeshData();
		for (int x = 0; x < chunkSize; x++) {
			for (int z = 0; z < chunkSize; z++) {
				for (int y = 0; y < chunkHeight; y++) {
					MeshData temp = blocks[x, y, z].draw(this, x, y, z, false);
					temp.addPos(new Vector3(x, y, z));
					d.add(temp);
				}
			}
		}

		mesh.vertices = d.verticies.ToArray();
		mesh.triangles = d.triangles.ToArray();
		mesh.uv = d.uvs.ToArray();
		mesh.Optimize();
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		GetComponent<MeshFilter>().mesh = mesh;
		GetComponent<MeshFilter>().sharedMesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}

	public void generateChunk() {
		blocks = new Block[chunkSize, chunkHeight, chunkSize];
		for (int x = 0; x < chunkSize; x++) {
			for (int z = 0; z < chunkSize; z++) {
				for (int y = 0; y < chunkHeight; y++) {
					blocks[x, y, z] = Block.newBlock("Air");
				}
			}
		}
	}

	public Block getBlock(int x, int y, int z) {
		if (x < 0 || x >= chunkSize) {
			return Block.newBlock("Air");
		}
		if (y < 0 || y >= chunkHeight) {
			return Block.newBlock("Air");
		}
		if (z < 0 || z >= chunkSize) {
			return Block.newBlock("Air");
		}
		return blocks[x, y, z];
	}

	public Vector3 posToBlock(Vector3 pos) {
		return new Vector3((int)Mathf.Floor(pos.x -= transform.position.x), (int)Mathf.Floor(pos.y), (int)Mathf.Floor(pos.z -= transform.position.z));
	}

	public void setBlock(Block block, int x, int y, int z) {
		blocks[x, y, z] = block;
		drawChunk();
	}

	int getY(int x, int z) {
		x += Mathf.RoundToInt(transform.position.x);
		z += Mathf.RoundToInt(transform.position.z);

		float perlin1 = Mathf.PerlinNoise(x / parts, z / parts) / div;
		float perlin2 = Mathf.PerlinNoise(z / parts, x / parts) / div;
		float perlin3 = Mathf.PerlinNoise(x / parts2, z / parts2) / div2;
		float perlin4 = Mathf.PerlinNoise(z / parts2, x / parts2) / div2;
		return Mathf.RoundToInt(perlin1 + perlin2 + perlin3 + perlin4 + add);
	}

	void Update() {

	}
}
