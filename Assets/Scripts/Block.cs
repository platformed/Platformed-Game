using UnityEngine;
using System.Collections;

public class Block {
	public Block(){

	}

	public MeshData draw(){
		Vertex[] v = new Vertex[]{
			new Vertex (0, 0, 0),	//0
			new Vertex (0, 0, 1),	//1
			new Vertex (0, 1, 0),	//2
			new Vertex (0, 1, 1),	//3
			new Vertex (1, 0, 0),	//4
			new Vertex (1, 0, 1),	//5
			new Vertex (1, 1, 0),	//6
			new Vertex (1, 1, 1)	//7
		};

		int[] t1 = new int[]{0, 1, 2, 3, 2, 1};
		int[] t2 = new int[]{0, 2, 1, 3, 1, 2};

		Vector2[] uvs = new Vector2[]{
			new Vector2(0, 0),
			new Vector2(0, 1),
			new Vector2(1, 0),
			new Vector2(1, 1)
		};

		Face bottom = new Face (new Vertex[]{v[0], v[1], v[4], v[5]}, t2, uvs);
		Face right = new Face (new Vertex[]{v[1], v[5], v[3], v[7]}, t1, uvs);
		Face left = new Face (new Vertex[]{v[0], v[4], v[2], v[6]}, t2, uvs);
		Face back = new Face (new Vertex[]{v[4], v[5], v[6], v[7]}, t2, uvs);
		Face front = new Face (new Vertex[]{v[0], v[1], v[2], v[3]}, t1, uvs);
		Face top = new Face (new Vertex[]{v[2], v[3], v[6], v[7]}, t1, uvs);

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
