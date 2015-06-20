using UnityEngine;
using System.Collections;

public class Block {
	public Block(){

	}

	public MeshData draw(){
		Vector3[] v = new Vector3[]{
			new Vertex (0, 0, 0).toVector(),	//0
			new Vertex (0, 0, 1).toVector(),	//1
			new Vertex (0, 1, 0).toVector(),	//2
			new Vertex (0, 1, 1).toVector(),	//3
			new Vertex (1, 0, 0).toVector(),	//4
			new Vertex (1, 0, 1).toVector(),	//5
			new Vertex (1, 1, 0).toVector(),	//6
			new Vertex (1, 1, 1).toVector()	//7
		};

		int[] t1 = new int[]{0, 1, 2, 3, 2, 1};
		int[] t2 = new int[]{0, 2, 1, 3, 1, 2};

		Vector2[] uvs = new Vector2[]{
			new Vector2(0, 0),
			new Vector2(0, 1),
			new Vector2(1, 0),
			new Vector2(1, 1)
		};

		Face bottom = new Face (new Vector3[]{v[0], v[1], v[4], v[5]}, t2, uvs);
		Face right = new Face (new Vector3[]{v[1], v[5], v[3], v[7]}, t1, uvs);
		Face left = new Face (new Vector3[]{v[0], v[4], v[2], v[6]}, t2, uvs);
		Face back = new Face (new Vector3[]{v[4], v[5], v[6], v[7]}, t2, uvs);
		Face front = new Face (new Vector3[]{v[0], v[1], v[2], v[3]}, t1, uvs);
		Face top = new Face (new Vector3[]{v[2], v[3], v[6], v[7]}, t1, uvs);

		MeshData data = new MeshData ();
		data.add (bottom.getMeshData());
		data.add (right.getMeshData());
		data.add (left.getMeshData());
		data.add (back.getMeshData());
		data.add (front.getMeshData());
		data.add (top.getMeshData());

		return data;
	}
}
