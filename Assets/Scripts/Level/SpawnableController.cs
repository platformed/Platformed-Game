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
			if(previousGamemode != UIManager.Gamemode) {
				block.Reset();
			}
			previousGamemode = UIManager.Gamemode;
			
			//Call Update and InactiveUpdate
			if (UIManager.Gamemode == Gamemode.Design) {
				block.InactiveUpdate();
			} else {
				block.Update();
			}
		}
	}

	//Call OnPlayerEnter
	void OnTriggerEnter(Collider coll) {
		if (UIManager.Gamemode == Gamemode.Play) {
			if (coll.CompareTag("Player")) {
				block.OnPlayerEnter();
			}
		}
	}
}
