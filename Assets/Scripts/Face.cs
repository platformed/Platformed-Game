using UnityEngine;
using System.Collections;

public class Face {
	public Vector3[] verticies;
	public int[] triangles;
	public Vector2[] uvs;

	public Face(Vector3[] v, int[] t, Vector2[] u){
		verticies = v;
		triangles = t;
		uvs = u;
	}

	public MeshData getMeshData(){
		return new MeshData (verticies, triangles, uvs);
	}
}
