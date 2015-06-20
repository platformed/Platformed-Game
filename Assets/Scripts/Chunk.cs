using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour {
	Block[ , , ] blocks;
	public static int chunkSize;

	void Start () {
		Block b = new Block ();
		Mesh mesh = new Mesh ();

		MeshData d = b.draw ();
		mesh.vertices = d.verticies;
		mesh.triangles = d.triangles;
		mesh.uv = d.uvs;
		mesh.RecalculateBounds ();
		mesh.RecalculateNormals ();
		GetComponent<MeshFilter> ().mesh = mesh;
		GetComponent<MeshFilter> ().sharedMesh = mesh;
	}

	void Update () {
	
	}
}
