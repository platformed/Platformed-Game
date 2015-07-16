using UnityEngine;
using System.Collections;

public class LevelInteract : MonoBehaviour {
	Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);
	float smooth = 20f;
	public GameObject target;
	public GameObject world;
	Block block = Block.testBlock1;

	void Start() {

	}
	
	void Update() {
		Vector3 hit = raycast ();

		if (Input.GetKey ("1")) {
			block = Block.testBlock1; 
		}
		if (Input.GetKey ("2")) {
			block = Block.testBlock2;
		}
		if (Input.GetKey ("3")) {
			block = Block.testBlock3;
		}

		if(Input.mousePosition.y < Screen.height - 50){
			//Smooth transition using lerp
			Vector3 newPos = new Vector3 (Mathf.Floor(hit.x), Mathf.Floor(hit.y), Mathf.Floor(hit.z)) + offset;
			transform.position = Vector3.Lerp (transform.position, newPos, Time.deltaTime * smooth);

			clampPos ();

			if (Input.GetMouseButton(0)) {
				World w = world.GetComponent<World>();
				Chunk c = w.posToChunk(hit);
				if(c != null){
					Vector3 p = c.posToBlock(hit);
					c.setBlock(block, (int) p.x, (int) p.y, (int) p.z);
				}
			}
				
			if (Input.GetMouseButton(1)) {
				World w = world.GetComponent<World>();
				Chunk c = w.posToChunk(hit);
				if(c != null){
					Vector3 p = c.posToBlock(hit);
					c.setBlock(Block.air, (int) p.x, (int) p.y, (int) p.z);
				}
			}
		}
	}

	//Raycasts from the mouse cursor to the plane that the camera target is on
	Vector3 raycast(){
		Plane plane = new Plane(Vector3.up, new Vector3(0, CameraMove.floor, 0));
		
		Vector3 hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		ray.direction = ray.direction * 1000;
		float distance;
		if (plane.Raycast(ray, out distance)) {
			hit = ray.GetPoint(distance);
			return hit;
			//Debug.Log("hit d:" + distance + " x:" + hit.x + " y:" + hit.y + " z:" + hit.z);
		}
		return new Vector3();
	}

	void clampPos() {
		float size = World.worldSize * Chunk.chunkSize - 0.5f;

		if (transform.position.x < 0) {
			transform.position = new Vector3(0.5f, transform.position.y, transform.position.z);
		}
		if (transform.position.x > size) {
			transform.position = new Vector3(size, transform.position.y, transform.position.z);
		}
		
		if (transform.position.z < 0) {
			transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);
		}
		if (transform.position.z > size) {
			transform.position = new Vector3(transform.position.x, transform.position.y, size);
		}
	}
}
