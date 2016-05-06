using UnityEngine;
using System.Collections;

public class SpawnableController : MonoBehaviour {
	SpawnableBlock block;
	Gamemode previousGamemode;

	public void SetBlock(SpawnableBlock b) {
		block = b;

		//Call reset when block created
		block.Reset();
	}
	
	void Update () {
		if (block != null) {
			//Call Reset
			if(previousGamemode != UIManager.gamemode) {
				block.Reset();
			}
			previousGamemode = UIManager.gamemode;
			
			//Call Update and InactiveUpdate
			if (UIManager.gamemode == Gamemode.Design) {
				block.InactiveUpdate();
			} else {
				block.Update();
			}
		}
	}

	//Call OnPlayerEnter
	void OnTriggerEnter(Collider coll) {
		if (UIManager.gamemode == Gamemode.Play) {
			if (coll.CompareTag("Player")) {
				block.OnPlayerEnter();
			}
		}
	}
}
