using UnityEngine;
using System.Collections;
using System;

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
			if(previousGamemode != GamemodeManager.instance.Gamemode) {
				block.Reset();
			}
			previousGamemode = GamemodeManager.instance.Gamemode;
			
			//Call Update and InactiveUpdate
			if (GamemodeManager.instance.Gamemode == Gamemode.Design) {
				block.InactiveUpdate();
			} else {
				block.Update();
			}

			//Set rotation
			transform.parent.rotation = Quaternion.Euler(0, 90 * block.GetRotation(), 0);
		}
	}

	//Call OnPlayerEnter
	void OnTriggerEnter(Collider coll) {
		if (GamemodeManager.instance.Gamemode == Gamemode.Play) {
			if (coll.CompareTag("Player")) {
				block.OnPlayerEnter();
			}
		}
	}
}
