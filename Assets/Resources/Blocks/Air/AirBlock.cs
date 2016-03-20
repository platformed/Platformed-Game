﻿using UnityEngine;
using System.Collections;

public class AirBlock : Block {
	public AirBlock(){
		name = "Air";
		displayName = "Air";
	}

	public override MeshData BlockData(int x, int y, int z, MeshData data, int submesh, Block[,,] blocks) {
		return data;
	}

	public override bool IsSolid(Direction direction) {
		return false;
	}
}
