using UnityEngine;
using System.Collections;

public class Vertex {
	public int x;
	public int y;
	public int z;

	public Vertex(int x, int y, int z){
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Vector3 toVector(){
		return new Vector3 (x, y, z);
	}
}
