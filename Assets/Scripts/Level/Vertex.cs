using UnityEngine;
using System.Collections;

public class Vertex {
	public int x;
	public int y;
	public int z;

	public Vertex(int xx, int yy, int zz){
		x = xx;
		y = yy;
		z = zz;
	}

	public Vector3 toVector(){
		return new Vector3 (x, y, z);
	}
}
