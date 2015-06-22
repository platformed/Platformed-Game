using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {
	public int worldSize = 10;
	public List<Chunk> chunks = new List<Chunk>();
	GameObject chunk;

	void Start () {
		chunk = Resources.Load ("Chunk") as GameObject;

		for(int x = 0; x < worldSize; x++){
			for(int z = 0; z < worldSize; z++){
				createChunk(x, z);
			}
		}
	}

	void createChunk(int x, int z){
		Transform t = Instantiate (chunk.transform, new Vector3 (x * Chunk.chunkSize, 0, z * Chunk.chunkSize), Quaternion.identity) as Transform;
		t.transform.name = "CX" + t.transform.position.x / Chunk.chunkSize + "Z" + t.transform.position.z / Chunk.chunkSize;
		t.transform.parent = this.transform;
		chunks.Add (t.GetComponent<Chunk> ());
	}

	void Update () {
	
	}
}
