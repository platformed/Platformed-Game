using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class World : MonoBehaviour {
	public Transform world;

	public static int worldSize = 10;
	public List<Chunk> chunks = new List<Chunk>();
	GameObject chunk;
	public static GameObject block;

	void Start () {
		world = transform;

		chunk = Resources.Load ("Chunk") as GameObject;
		block = Resources.Load ("Block") as GameObject;

		for (int x = 0; x < worldSize; x++){
			for(int z = 0; z < worldSize; z++){
				createChunk(x, z);
			}
		}
	}

	public Chunk posToChunk(Vector3 pos){
		foreach (Chunk c in chunks) {
			if((int) Mathf.Floor(pos.x / Chunk.chunkSize) == (int) Mathf.Floor(c.gameObject.transform.position.x / Chunk.chunkSize)) {
				if((int) Mathf.Floor(pos.z / Chunk.chunkSize) == (int) Mathf.Floor(c.gameObject.transform.position.z / Chunk.chunkSize)) {
					return c;
				}
			}
		}
		return null;
	}

	void createChunk(int x, int z){
		Transform t = Instantiate (chunk.transform, new Vector3 (x * Chunk.chunkSize, 0, z * Chunk.chunkSize), Quaternion.identity) as Transform;
		t.name = "Chunk X" + t.position.x / Chunk.chunkSize + " Z" + t.position.z / Chunk.chunkSize;
		t.parent = world;
		chunks.Add (t.GetComponent<Chunk> ());
	}

	public void saveWorld(string name){
		FileStream stream = File.Open (Application.persistentDataPath + "\\" + name, FileMode.Create);

		Block.saveBlockIDs(stream);

		foreach(Chunk c in chunks){
			c.save(stream);
		}

		stream.Close ();
	}

	public void loadWorld(string name){
		FileStream stream = File.Open (Application.persistentDataPath + "\\" + name, FileMode.Open);

		Block.LoadBlockIDs(stream);
		
		foreach(Chunk c in chunks) {
			c.load(stream);
		}
		
		stream.Close ();
	}

	public void newWorld() {
		foreach (Chunk c in chunks) {
			c.generateChunk();
			c.drawChunk();
		}
	}

	void Update () {
	
	}
}
