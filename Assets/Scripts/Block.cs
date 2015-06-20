using UnityEngine;
using System.Collections;

public class Block {
	public static Block air = new Block(0, "air", true);
	public static Block block = new Block(1, "block", false);

	public string name;
	public int id;
	public bool transparent;

	public Block(int i, string n, bool t){
		name = n;
		id = i;
		transparent = t;
	}

	public bool isTransparent(){
		return transparent;
	}

	public MeshData draw(Chunk chunk, int x, int y, int z){
		if(this.id == 0){
			return new MeshData(new Vector3[]{}, new int[]{}, new Vector2[]{});
		}

		Vector3[] v = new Vector3[]{
			new Vertex (0, 0, 0).toVector(),	//0
			new Vertex (0, 0, 1).toVector(),	//1
			new Vertex (0, 1, 0).toVector(),	//2
			new Vertex (0, 1, 1).toVector(),	//3
			new Vertex (1, 0, 0).toVector(),	//4
			new Vertex (1, 0, 1).toVector(),	//5
			new Vertex (1, 1, 0).toVector(),	//6
			new Vertex (1, 1, 1).toVector()		//7
		};

		int[] t1 = new int[]{0, 1, 2, 3, 2, 1};
		int[] t2 = new int[]{0, 2, 1, 3, 1, 2};

		Vector2[] uvs = new Vector2[]{
			new Vector2(0, 0),
			new Vector2(0, 1),
			new Vector2(1, 0),
			new Vector2(1, 1)
		};

		MeshData data = new MeshData ();

		if (chunk.getBlock(x, y - 1, z).isTransparent()){
			Face bottom = new Face (new Vector3[]{v[0], v[1], v[4], v[5]}, t2, uvs);
			data.add (bottom.getMeshData());
		}

		if (chunk.getBlock(x, y, z + 1).isTransparent()){
			Face right = new Face (new Vector3[]{v[1], v[5], v[3], v[7]}, t1, uvs);
			data.add (right.getMeshData());
		}

		if (chunk.getBlock (x, y, z - 1).isTransparent ()) {
			Face left = new Face (new Vector3[]{v [0], v [4], v [2], v [6]}, t2, uvs);
			data.add (left.getMeshData ());
		}

		if (chunk.getBlock (x + 1, y, z).isTransparent ()) {
			Face back = new Face (new Vector3[]{v [4], v [5], v [6], v [7]}, t2, uvs);
			data.add (back.getMeshData ());
		}

		if (chunk.getBlock (x - 1, y, z).isTransparent ()) {
			Face front = new Face (new Vector3[]{v [0], v [1], v [2], v [3]}, t1, uvs);
			data.add (front.getMeshData ());
		}

		if (chunk.getBlock (x, y + 1, z).isTransparent ()) {
			Face top = new Face (new Vector3[]{v [2], v [3], v [6], v [7]}, t1, uvs);
			data.add (top.getMeshData ());
		}

		if (data.verticies == null) {
			return new MeshData(new Vector3[]{}, new int[]{}, new Vector2[]{});
		}

		data.addPos (new Vector3 (0.5f, 0.5f, 0.5f));
		return data;
	}
}
