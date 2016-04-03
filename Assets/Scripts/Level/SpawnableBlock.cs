using UnityEngine;
using System.Collections;

public class SpawnableBlock : Block {
	public override MeshData BlockData(int x, int y, int z, MeshData data, int submesh, Block[,,] blocks) {
		return data;
	}

	public override BlockSolidity GetSolidity(Direction direction) {
		return BlockSolidity.None;
	}
}
