using UnityEngine;
using System.Collections;

public class AirBlock : Block {
	public AirBlock(){
		name = "Air";
		displayName = "Air";
	}

	public override MeshData BlockData(int x, int y, int z, MeshData data, int submesh, Block[,,] blocks) {
		return data;
	}

	public override BlockSolidity GetSolidity(Direction direction) {
		return BlockSolidity.None;
	}

	public override void InstantiateBlock(Transform parent, Vector3 pos, int x, int y, int z, Block[,,] blocks) {
		return;
	}

	public override void DestroyBlock() {
		return;
	}

	public override Collider GetCollider(GameObject parent, Vector3 pos) {
		return null;
	}
}
