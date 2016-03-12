using UnityEngine;
using System.Collections;

public class BlockLayer {
	public Color[,] colorMap;
	public LayerBlendMode blendMode;
	public float opacity;

	public BlockLayer(LayerBlendMode b) {
		colorMap = new Color[BlockEditor.textureSize, BlockEditor.textureSize];
		blendMode = b;
		opacity = 1f;

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

	public void DrawCircle(int xCenter, int yCenter, float radius, Color color) {
		for (int x = 0; x < colorMap.GetLength(0); x++) {
			for (int y = 0; y < colorMap.GetLength(1); y++) {
				if (Mathf.Sqrt(((xCenter - x) * (xCenter - x)) + ((yCenter - y) * (yCenter - y))) < radius) {
					colorMap[x, y] = color;
				}
			}
		}
	}

	public void DrawSquare(int xCenter, int yCenter, int radius, Color color) {
		for (int x = xCenter - radius; x < xCenter + radius; x++) {
			for (int y = yCenter - radius; y < yCenter + radius; y++) {
				colorMap[x, y] = color;
			}
		}
	}
}
