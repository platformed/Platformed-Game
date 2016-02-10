using System;
using UnityEngine;

/// <summary>
/// A cursor that shows where you are placing blocks
/// </summary>
public class Cursor : MonoBehaviour {
	Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);
	float smooth = 30f;
	public World world;
	public static Block block;

	new MeshRenderer renderer;
	MeshFilter filter;

	void Start() {
		block = new TestBlock();
		renderer = GetComponent<MeshRenderer>();
		filter = GetComponent<MeshFilter>();
	}

	void Update() {
		RenderCursor();

		Vector3 newPos = new Vector3(0, 0, 0);

		if (UIManager.tool == Tool.BLOCK && UIManager.canInteract()) {
			Vector3 hit = UIManager.raycast();
			newPos = new Vector3(Mathf.Floor(hit.x), Mathf.Floor(hit.y), Mathf.Floor(hit.z)) + offset;

			if (Input.GetMouseButton(0)) {
				world.SetBlock((int) hit.x, (int) hit.y, (int) hit.z, block);
			}

			if (Input.GetMouseButton(1)) {
				world.SetBlock((int)hit.x, (int)hit.y, (int)hit.z, new AirBlock());
			}
		}

		transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * smooth);
		clampPos();
	}

	void RenderCursor() {
		MeshData data = new MeshData();
		block.BlockData(null, 0, 0, 0, data, true);

		filter.mesh.Clear();
		filter.mesh.vertices = data.vertices.ToArray();
		filter.mesh.triangles = data.triangles.ToArray();
		filter.mesh.uv = data.uvs.ToArray();
		filter.mesh.normals = data.normals.ToArray();
	}

	void clampPos() {
		float size = UIManager.worldSize;

		renderer.enabled = true;

		if (transform.position.x < 0) {
			renderer.enabled = false;
			//transform.position = new Vector3(0.5f, transform.position.y, transform.position.z);
		}
		if (transform.position.x > size) {
			renderer.enabled = false;
			//transform.position = new Vector3(size, transform.position.y, transform.position.z);
		}

		if (transform.position.z < 0) {
			renderer.enabled = false;
			//transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);
		}
		if (transform.position.z > size) {
			renderer.enabled = false;
			//transform.position = new Vector3(transform.position.x, transform.position.y, size);
		}
	}
}
