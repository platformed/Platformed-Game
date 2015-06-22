using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour {
	Block[ , , ] blocks;
	public static int chunkSize = 10;
	public static int chunkHeight = 20;

	float parts = 20f;
	float div = 0.1f;
	float parts2 = 2.25f;
	float div2 = 0.85f;

	void Start () {
		drawChunk ();
	}

	void drawChunk(){
		Mesh mesh = new Mesh ();
		
		blocks = new Block[chunkSize, chunkHeight, chunkSize];
		for (int x = 0; x < chunkSize; x++) {
			for (int z = 0; z < chunkSize; z++) {
				int height = getY(x, z);
				for (int y = 0; y < chunkHeight; y++) {
					Block b;
					if(y <= height) {
						b = Block.block;
					} else {
						b = Block .air;
					}
					blocks[x, y, z] = b;
				}
			}
		}

		MeshData d = new MeshData ();
		for (int x = 0; x < chunkSize; x++) {
			for (int z = 0; z < chunkSize; z++) {
				for (int y = 0; y < chunkHeight; y++) {
					MeshData temp = blocks[x, y, z].draw(this, x, y, z);
					temp.addPos(new Vector3(x, y, z));
					d.add(temp);
				}
			}
		}

		
		mesh.vertices = d.verticies.ToArray();
		mesh.triangles = d.triangles.ToArray();
		mesh.uv = d.uvs.ToArray();
		mesh.Optimize ();
		mesh.RecalculateBounds ();
		mesh.RecalculateNormals ();
		GetComponent<MeshFilter> ().mesh = mesh;
		GetComponent<MeshFilter> ().sharedMesh = mesh;
	}

	public Block getBlock(int x, int y, int z){
		if (x < 0 || x >= chunkSize) {
			return Block.air;
		}
		if (y < 0 || y >= chunkHeight) {
			return Block.air;
		}
		if (z < 0 || z >= chunkSize) {
			return Block.air;
		}
		return blocks [x, y, z];
	}

	int getY(int x, int z){
		x += Mathf.RoundToInt(transform.position.x);
		z += Mathf.RoundToInt(transform.position.z);

		float perlin1 = Mathf.PerlinNoise (x / parts, z / parts) / div;
		float perlin2 = Mathf.PerlinNoise (z / parts, x / parts) / div;
		float perlin3 = Mathf.PerlinNoise (x / parts2, z / parts2) / div2;
		float perlin4 = Mathf.PerlinNoise (z / parts2, x / parts2) / div2;
		return Mathf.RoundToInt(perlin1 + perlin2 + perlin3 + perlin4);
	}

	void Update () {

	}
}
