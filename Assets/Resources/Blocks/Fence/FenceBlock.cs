using UnityEngine;
using System.Collections;

public class FenceBlock : ModelBlock {
	public FenceBlock() {
		Name = "Fence";
		DisplayName = "Fence";
	}

	/*public override MeshData BlockData(int x, int y, int z, MeshData data, int submesh, Block[,,] blocks) {
		if (blocks[x, y, z] is FenceBlock) {

		}
	}*/
}
