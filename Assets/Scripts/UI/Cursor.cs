using UnityEngine;

/// <summary>
/// A cursor that shows where you are placing blocks
/// </summary>
public class Cursor : MonoBehaviour {
	Vector3 offset = new Vector3(0, 0, 0);
	float smooth = 20f;
	public World world;
	public static Block block;

	void Start() {
		block = new Block();
	}

	void Update() {
		Vector3 newPos = new Vector3(0, 0, 0);

		if (UIManager.tool == Tool.BLOCK && UIManager.canInteract()) {
			Vector3 hit = UIManager.raycast();
			newPos = new Vector3(Mathf.Floor(hit.x), Mathf.Floor(hit.y), Mathf.Floor(hit.z)) + offset;

			if (Input.GetMouseButton(0)) {
				world.SetBlock((int) hit.x, (int) hit.y, (int) hit.z, block);
			}

			if (Input.GetMouseButton(1)) {
				world.SetBlock((int)hit.x, (int)hit.y, (int)hit.z, new BlockAir());
			}
		}

		transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * smooth);
		clampPos();
	}

	void clampPos() {
		MeshRenderer renderer = GetComponent<MeshRenderer>();
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
