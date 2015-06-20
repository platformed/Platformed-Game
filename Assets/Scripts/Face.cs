using UnityEngine;
using System.Collections;

public class Face {
	public Vector3[] verticies;
	public int[] triangles;
	public Vector2[] uvs;

	public Face(Vertex[] v, int[] t, Vector2[] u){
		verticies = new Vector3[v.Length];
		int count = 0;
		foreach (Vertex i in v) {
			verticies[count] = new Vector3(i.x, i.y, i.z);
			count++;
		}
		triangles = t;
		uvs = u;
	}

	public MeshData getMeshData(){
		return new MeshData (verticies, triangles, uvs);
	}
}
