using UnityEngine;
using System.Collections;

public class BlockLayer {
	Color[,] colorMap;
	public LayerBlendMode blendMode;

	public BlockLayer(LayerBlendMode b) {
		colorMap = new Color[BlockEditor.textureSize, BlockEditor.textureSize];
		blendMode = b;

		//Temp
		for (int x = 0; x < colorMap.GetLength(0); x++) {
			for (int y = 0; y < colorMap.GetLength(1); y++) {
				//colorMap[x, y] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
				colorMap[x, y] = new Color(0f, 0f, 0f, 0f);
			}
		}
	}

	public Color[,] Render() {
		return colorMap;
	}
}
