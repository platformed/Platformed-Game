﻿using UnityEngine;
using System.Collections;

public class GridRenderer : MonoBehaviour {
	float offset = 0.01f;
	bool visible;

	void Start () {
		//Scale grid to the size of the world
		transform.localScale = new Vector3 (World.worldSize * Chunk.chunkSize / 10, 1, World.worldSize * Chunk.chunkSize / 10);

		//Set position to the center of the world
		transform.position = new Vector3 (World.worldSize * Chunk.chunkSize / 2, CameraMove.floor + offset, World.worldSize * Chunk.chunkSize / 2);
	}

	void Update () {
		//Smooth transition with lerp
		transform.position = new Vector3 (transform.position.x, Mathf.Lerp(transform.position.y, CameraMove.floor + offset, Time.deltaTime * CameraMove.smooth), transform.position.z);
	}
}
