using UnityEngine;
using System.Collections;

public class GrassBlock : SpawnableBlock {
	//GameObject grass;
	//const float renderDistance = 20f;

	public GrassBlock() {
		Name = "Grass";
		DisplayName = "Grass Floor";
	}

	/*public override void Spawn() {
		grass = transform.FindChild("Grass").gameObject;
	}

	public override void InactiveUpdate() {
		if(Vector3.Distance(UIManager.instance.designCam.transform.position, transform.position) > renderDistance) {
			grass.SetActive(false);
		} else {
			grass.SetActive(true);
		}
	}*/
}
