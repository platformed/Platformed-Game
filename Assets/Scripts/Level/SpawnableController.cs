using UnityEngine;
using System.Collections;

public class SpawnableController : MonoBehaviour {
	SpawnableBlock block;

	public void SetBlock(SpawnableBlock b) {
		block = b;
	}
	
	void Update () {
		if (block != null) {
			//TODO: Call Reset
			//TODO: Call OnPlayerCollision

			if (UIManager.gamemode == Gamemode.Design) {
				block.InactiveUpdate();
			} else {
				block.Update();
			}
		}
	}
}
