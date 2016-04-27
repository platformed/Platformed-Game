using UnityEngine;
using System.Collections;

public class SpawnableController : MonoBehaviour {
	SpawnableBlock block;
	Gamemode previousGamemode;

	public void SetBlock(SpawnableBlock b) {
		block = b;
	}
	
	void Update () {
		if (block != null) {
			if(UIManager.gamemode != previousGamemode) {
				if (UIManager.gamemode == Gamemode.Design) {
					block.InactiveStart();
				} else {
					block.Start();
				}
			}
			previousGamemode = UIManager.gamemode;

			if (UIManager.gamemode == Gamemode.Design) {
				block.InactiveUpdate();
			} else {
				block.Update();
			}
		}
	}
}
