using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.CodeDom;

public class MeshData{
	/*public List<Vector3> vertices = new List<Vector3> ();
	public List<int> triangles = new List<int> ();
	public List<Vector2> uv = new List<Vector2> ();*/
	
	public Vector3[] verticies;
	public int[] triangles;
	public Vector2[] uvs;


	public MeshData(){

	}

	public MeshData(Vector3[] v, int[] t, Vector2[] u){
		verticies = v;
		triangles = t;
		uvs = u;
	}

	//Add another meshdata to this one
	public void add(MeshData d){
		//If this meshdata is null, just set it
		if (verticies == null) {
			verticies = d.verticies;
			triangles = d.triangles;
			uvs = d.uvs;
			return;
		}

		//Convert to lists and add
		List<Vector3> v = new List<Vector3> ();
		List<int> t = new List<int> ();
		List<Vector2> u = new List<Vector2> ();

		int count = verticies.Length;

		foreach (Vector3 i in verticies) {
			v.Add(i);
		}
		foreach (int i in triangles) {
			t.Add(i);
		}
		foreach (Vector2 i in uvs) {
			u.Add(i);
		}

		foreach (Vector3 i in d.verticies) {
			v.Add(i);
		}
		for(int i = 0; i < d.triangles.Length;  i++){
			t.Add(d.triangles[i] + count);
		}
		foreach (Vector2 i in d.uvs) {
			u.Add(i);
		}

		verticies = v.ToArray ();
		triangles = t.ToArray ();
		uvs = u.ToArray ();
	}
}
