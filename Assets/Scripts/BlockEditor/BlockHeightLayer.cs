using UnityEngine;
using System.Collections;

public class BlockHeightLayer {
	public float[,] heightMap;
	LayerBlendMode blendMode;

	public BlockHeightLayer() {
		heightMap = new float[BlockEditor.textureSize, BlockEditor.textureSize];

		//Temp
		for (int x = 0; x < heightMap.GetLength(0); x++) {
			for (int y = 0; y < heightMap.GetLength(1); y++) {
				heightMap[x, y] = Random.Range(0f, 1f);
			}
		}
	}

	public Color[,] Render() {
		Color[,] c = new Color[BlockEditor.textureSize, BlockEditor.textureSize];

		for (int x = 0; x < c.GetLength(0); x++) {
			for (int y = 0; y < c.GetLength(1); y++) {
				c[x, y] = new Color(heightMap[x, y], heightMap[x, y], heightMap[x, y]);
			}
		}

		return c;
	}
}
