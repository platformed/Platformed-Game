﻿using UnityEngine;
using System.Collections;

public class SelectTool : MonoBehaviour {
	public World world;
	public BlockCursor cursor;
	Vector3 pos1;
	Vector3 pos2;
	const float offset = 0.01f;
	MeshRenderer meshRenderer;

	void Start() {
		meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.material.renderQueue = 3001;
	}

	void Update() {
		if (DesignManager.instance.tool == Tool.Select && DesignManager.instance.CanInteractLevel()) {
			if (Input.GetMouseButtonDown(0)) {
				pos1 = Round(CameraOrbit.instance.Raycast());
			}
			if (Input.GetMouseButton(0)) {
				pos2 = Round(CameraOrbit.instance.Raycast());
			}

			if (Input.GetKeyDown(KeyCode.C)) {
				Copy();
			}

			if (Input.GetKeyDown(KeyCode.X)) {
				Copy();
				Delete();
			}

			if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace)) {
				Delete();
			}
		}

		Render();
	}

	void Render() {
		transform.position = new Vector3((pos1.x + pos2.x) / 2 + 0.5f, (pos1.y + pos2.y) / 2 + 0.5f, (pos1.z + pos2.z) / 2 + 0.5f);

		float x = pos2.x - pos1.x;
		float y = pos2.y - pos1.y;
		float z = pos2.z - pos1.z;

		//Add extra scale
		if (x > 0) {
			x += 1 + offset;
		} else {
			x -= 1 + offset;
		}

		if (y > 0) {
			y += 1 + offset;
		} else {
			y -= 1 + offset;
		}

		if (z > 0) {
			z += 1 + offset;
		} else {
			z -= 1 + offset;
		}

		transform.localScale = new Vector3(x, y, z);

		if(DesignManager.instance.tool == Tool.Select) {
			meshRenderer.enabled = true;
		} else {
			meshRenderer.enabled = false;
		}
	}

	Vector3 Round(Vector3 v) {
		return new Vector3((int)v.x, (int)v.y, (int)v.z);
	}

	/// <summary>
	/// Copys the contents of the selection to the cursor
	/// </summary>
	public void Copy() {
		Block[,,] blocks;

		Vector3 p1;
		Vector3 p2;

		if (pos1.x < pos2.x) {
			p1.x = pos1.x;
			p2.x = pos2.x;
		} else {
			p1.x = pos2.x;
			p2.x = pos1.x;
		}

		if (pos1.y < pos2.y) {
			p1.y = pos1.y;
			p2.y = pos2.y;
		} else {
			p1.y = pos2.y;
			p2.y = pos1.y;
		}

		if (pos1.z < pos2.z) {
			p1.z = pos1.z;
			p2.z = pos2.z;
		} else {
			p1.z = pos2.z;
			p2.z = pos1.z;
		}
		
		p2 += Vector3.one;

		blocks = new Block[(int) (p2.x - p1.x), (int) (p2.y - p1.y), (int) (p2.z - p1.z)];

		for (int x = (int)p1.x; x < (int)p2.x; x++) {
			for (int y = (int)p1.y; y < (int)p2.y; y++) {
				for (int z = (int)p1.z; z < (int)p2.z; z++) {
					blocks[(int) (x - p1.x), (int) (y - p1.y), (int) (z - p1.z)] = world.GetBlock(x, y, z).Copy();
				}
			}
		}
		
		Vector3 offset = CameraOrbit.instance.Raycast();
		cursor.Copy(blocks, new Vector3(Mathf.Floor(offset.x) - p1.x, Mathf.Floor(offset.y) - p1.y, Mathf.Floor(offset.z) - p1.z));

		DesignManager.instance.tool = Tool.Block;
	}

	/// <summary>
	/// Replaces the selection with air
	/// </summary>
	void Delete() {
		Vector3 p1;
		Vector3 p2;

		if (pos1.x < pos2.x) {
			p1.x = pos1.x;
			p2.x = pos2.x;
		} else {
			p1.x = pos2.x;
			p2.x = pos1.x;
		}

		if (pos1.y < pos2.y) {
			p1.y = pos1.y;
			p2.y = pos2.y;
		} else {
			p1.y = pos2.y;
			p2.y = pos1.y;
		}

		if (pos1.z < pos2.z) {
			p1.z = pos1.z;
			p2.z = pos2.z;
		} else {
			p1.z = pos2.z;
			p2.z = pos1.z;
		}

		p2 += Vector3.one;

		for (int x = (int)p1.x; x < (int)p2.x; x++) {
			for (int y = (int)p1.y; y < (int)p2.y; y++) {
				for (int z = (int)p1.z; z < (int)p2.z; z++) {
					world.SetBlock(x, y, z, new AirBlock());
				}
			}
		}
	}
}
