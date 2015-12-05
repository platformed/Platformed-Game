using UnityEngine;
using System.Collections;

public class SelectBox : MonoBehaviour {
	Vector3 start;
	Vector3 end;
	Vector3 p1;
	Vector3 p2;
	
	public World world;
	Block[,,] clipboard;

	void Update() {
		if (UIManager.tool == Tool.SELECT && UIManager.canInteract()) {
			if (Input.GetMouseButtonDown(0)) {
				start = UIManager.raycast();
			}
			if (Input.GetMouseButton(0)) {
				end = UIManager.raycast();
			}
		}

		p1 = round1(start);
		p2 = round2(end);

		setPosition();
	}

	public void copy() {
		Vector3 c1 = new Vector3(lower(p1.x, p2.x), lower(p1.y, p2.y), lower(p1.z, p2.z));
		Vector3 c2 = new Vector3(higher(p1.x, p2.x), higher(p1.y, p2.y), higher(p1.z, p2.z));

		Debug.Log(c1.ToString() + " " + c2.ToString());
		Cursor.block = new Block[(int)(c2.x - c1.x) - 1, (int)(c2.y - c1.y) - 1, (int)(c2.z - c1.z) - 1];

		for (int x = (int) c1.x; x < c2.x - 1; x++) {
			for (int y = (int)c1.y; y < c2.y - 1; y++) {
				for (int z = (int)c1.z; z < c2.z - 1; z++) {
					Chunk chunk = world.posToChunk(new Vector3(x, y, z));
					Vector3 pos = chunk.posToBlock(new Vector3(x, y, z));

					//Debug.Log("array size: x:" + Cursor.block.GetLength(0) + " y:" + Cursor.block.GetLength(1) + " z:" + Cursor.block.GetLength(2));
					//Debug.Log("array pos:" + new Vector3((int)(x - chunk.transform.position.x - pos.x), (int)(y - chunk.transform.position.y - pos.y), (int)(z - chunk.transform.position.z - pos.z)).ToString() + " block pos:" + pos.ToString());
					//Debug.Log("array pos:" + new Vector3((int)(x - c1.x), (int)(y - c1.y), (int)(z - c1.z)).ToString() + " block pos:" + pos.ToString());
					Debug.Log(chunk.getBlock((int)pos.x, (int)pos.y, (int)pos.z).getBlockType().getDisplayName());
					Cursor.block[(int)(x - c1.x), (int)(y - c1.y), (int)(z - c1.z)] = chunk.getBlock((int)pos.x, (int)pos.y, (int)pos.z);
				}
			}
		}
	}

	int lower(float a, float b) {
		if (a < b)
			return Mathf.FloorToInt(a);
		else
			return Mathf.FloorToInt(b);
	}

	int higher(float a, float b) {
		if (a > b)
			return Mathf.FloorToInt(a);
		else
			return Mathf.FloorToInt(b);
	}

	void setPosition() {
		transform.position = new Vector3((p1.x + p2.x) / 2, (p1.y + p2.y) / 2, (p1.z + p2.z) / 2);
		transform.localScale = new Vector3(p2.x - p1.x, p2.y - p1.y, p2.z - p1.z);
	}

	Vector3 round1(Vector3 v) {
		float x = (int)Mathf.Floor(v.x);
		float y = (int)Mathf.Floor(v.y);
		float z = (int)Mathf.Floor(v.z);

		if (v.x > p2.x) {
			x += 1.01f;
		} else {
			x -= 0.01f;
		}

		if (v.y > p2.y) {
			y += 1.01f;
		} else {
			y -= 0.01f;
		}

		if (v.z > p2.z) {
			z += 1.01f;
		} else {
			z -= 0.01f;
		}

		return new Vector3(x, y, z);
	}

	Vector3 round2(Vector3 v) {
		float x = (int)Mathf.Floor(v.x);
		float y = (int)Mathf.Floor(v.y);
		float z = (int)Mathf.Floor(v.z);

		if (v.x >= p1.x) {
			x += 1.01f;
		} else {
			x -= 0.01f;
		}

		if (v.y >= p1.y) {
			y += 1.01f;
		} else {
			y -= 0.01f;
		}

		if (v.z >= p1.z) {
			z += 1.01f;
		} else {
			z -= 0.01f;
		}

		return new Vector3(x, y, z);
	}
}
