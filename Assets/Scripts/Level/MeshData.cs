using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.CodeDom;

public class MeshData{
	public List<Vector3> verticies = new List<Vector3> ();
	public List<int> triangles = new List<int> ();
	public List<Vector2> uvs = new List<Vector2> ();

	public MeshData(){

	}

	public MeshData(Vector3[] v, int[] t, Vector2[] u){
		foreach (Vector3 i in v) {
			verticies.Add(i);
		}
		foreach (int i in t) {
			triangles.Add(i);
		}
		foreach (Vector2 i in u) {
			uvs.Add(i);
		}
	}

	public void addPos(Vector3 p){
		for(int i = 0; i < verticies.Count; i++){
			verticies[i] += p;
		}
	}

	//Add another meshdata to this one
	public void add(MeshData d){
		//If this meshdata is null, just set it
		if(verticies == null){
			verticies = d.verticies;
			triangles = d.triangles;
			uvs = d.uvs;
			return;
		}

		//Add the other mesh
		int count = verticies.Count;
		foreach (Vector3 i in d.verticies) {
			verticies.Add(i);
		}
		foreach (int i in d.triangles) {
			triangles.Add(i + count);
		}
		foreach (Vector3 i in d.uvs) {
			uvs.Add(i);
		}
	}
}
