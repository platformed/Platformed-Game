using UnityEngine;
using System.Collections;

public struct WorldPos {
	public int x, y, z;

	public WorldPos(int x, int y, int z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public override bool Equals(object obj) {
		//Return false if not a WorldPos
		if (!(obj is WorldPos))
			return false;

		WorldPos pos = (WorldPos) obj;

		//Check if the positions match
		if(pos.x != x || pos.y != y || pos.z != z) {
			return false;
		} else {
			return true;
		}
	}
}
