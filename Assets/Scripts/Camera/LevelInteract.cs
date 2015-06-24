using UnityEngine;
using System.Collections;

public class LevelInteract : MonoBehaviour {
	public GameObject target;
	public GameObject world;

	void Start() {

	}
	
	void Update() {

		if (Input.GetMouseButton(0)) {
			Vector3 hit = raycast();

			World w = world.GetComponent<World>();
			Chunk c = w.posToChunk(hit);
			if(c != null){
				Vector3 p = c.posToBlock(hit);
				c.setBlock(Block.block, (int) p.x, (int) p.y, (int) p.z);
			}
		}

		if (Input.GetMouseButton(1)) {
			Vector3 hit = raycast();
			
			World w = world.GetComponent<World>();
			Chunk c = w.posToChunk(hit);
			if(c != null){
				Vector3 p = c.posToBlock(hit);
				c.setBlock(Block.air, (int) p.x, (int) p.y, (int) p.z);
			}
		}
	}

	//Raycasts from the mouse cursor to the plane that the camera target is on
	Vector3 raycast(){
		Plane plane = new Plane(Vector3.up, new Vector3(0, target.transform.position.y, 0));
		
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
}
