using UnityEngine;
using System.Collections;

public class FlagStartBlock : SpawnableBlock {
	public FlagStartBlock() {
		Name = "FlagStart";
		DisplayName = "Start Flag";
	}

	public override void InstantiateBlock(Transform parent, Vector3 pos, int x, int y, int z, Block[,,] blocks) {
		base.InstantiateBlock(parent, pos, x, y, z, blocks);
		World.instance.startFlagPosition = pos;
	}
}
