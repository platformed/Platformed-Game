﻿using UnityEngine;
using System.Collections;

public class BlockAir : Block {
	public BlockAir() : base() {

	}

	public override MeshData BlockData(Chunk chunk, int x, int y, int z, MeshData data) {
		return data;
	}

	public override bool IsSolid(Direction direction) {
		return false;
	}
}
