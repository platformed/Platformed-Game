using UnityEngine;

public class Cursor : MonoBehaviour {
	Vector3 offset = new Vector3(0, 0, 0);
	float smooth = 20f;
	public GameObject target;
	public GameObject world;
	public static Block block = Block.testBlock1;

	void Update() {
		gameObject.SetActive(true);

		drawBlock();

		Vector3 newPos = new Vector3(0, 0, 0);

		if (UIManager.tool == Tool.BLOCK && UIManager.canInteract()) {
			Vector3 hit = UIManager.raycast();
			newPos = new Vector3(Mathf.Floor(hit.x), Mathf.Floor(hit.y), Mathf.Floor(hit.z)) + offset;

			if (Input.GetMouseButton(0)) {
				World w = world.GetComponent<World>();
				Chunk c = w.posToChunk(hit);
				if (c != null) {
					Vector3 p = c.posToBlock(hit);
					c.setBlock(block, (int)p.x, (int)p.y, (int)p.z);
				}
			}

			if (Input.GetMouseButton(1)) {
				World w = world.GetComponent<World>();
				Chunk c = w.posToChunk(hit);
				if (c != null) {
					Vector3 p = c.posToBlock(hit);
					c.setBlock(Block.air, (int)p.x, (int)p.y, (int)p.z);
				}
			}
		}

		transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * smooth);
		clampPos();
	}

	void drawBlock() {
		MeshData d = block.draw(null, 0, 0, 0, true);
		Mesh mesh = new Mesh();
		mesh.vertices = d.verticies.ToArray();
		mesh.triangles = d.triangles.ToArray();
		mesh.uv = d.uvs.ToArray();
		mesh.Optimize();
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		GetComponent<MeshFilter>().mesh = mesh;
		GetComponent<MeshFilter>().sharedMesh = mesh;
	}

	void clampPos() {
		MeshRenderer renderer = GetComponent<MeshRenderer>();
		float size = World.worldSize * Chunk.chunkSize - 0.5f;

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
