using UnityEngine;
using System.Collections;

public class SticksBlock : ModelBlock {
	public SticksBlock() {
		Name = "Sticks";
		DisplayName = "Sticks";
	}

	public override Collider GetCollider(GameObject parent, Vector3 pos) {
		return new FloorBlock().GetCollider(parent, pos);
	}
}
