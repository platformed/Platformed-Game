using UnityEngine;
using System.Collections;

public class BricksBlock : Block {
	[Property("Size", "The size of the block")]
	public float Size { get; set; }

	[Property("Tint color", "Color to tint the block")]
	public Color Color { get; set; }

	public BricksBlock() {
		Name = "Bricks";
		DisplayName = "Bricks";
	}
}
