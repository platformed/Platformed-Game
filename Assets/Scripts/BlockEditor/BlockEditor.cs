using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockEditor : MonoBehaviour {
	public static int textureSize = 10;

	List<BlockLayer> layers = new List<BlockLayer>();
	public Material material;

	bool update;

	void Start() {
		layers.Add(new BlockLayer(LayerBlendMode.Normal));

		update = true;
	}

	void Update() {
		if (update) {
			material.mainTexture = Render();
			update = false;
		}

		//Set camera viewport rect
		int h = Screen.height;
		int w = Screen.width;
		int right = 300;
		int top = 64;
		Camera.main.rect = new Rect(0, 0, (w - right) / (float)w, (h - top) / (float)h);
	}

	Texture2D Render() {
		Color[,] texture = new Color[textureSize, textureSize];

		List<Color[,]> layerColors = new List<Color[,]>();
		List<LayerBlendMode> layerBlendModes = new List<LayerBlendMode>();

		foreach (BlockLayer l in layers) {
			layerColors.Add(l.Render());
			layerBlendModes.Add(l.blendMode);
		}

		for (int i = 0; i < layerColors.Count; i++) {
			for (int x = 0; x < textureSize; x++) {
				for (int y = 0; y < textureSize; y++) {
					switch (layerBlendModes[i]) {
						case LayerBlendMode.Normal:
							texture[x, y] = layerColors[i][x, y];
							break;
					}
				}
			}
		}

		Color[] colors = new Color[textureSize * textureSize];

		for (int x = 0; x < textureSize; x++) {
			for (int y = 0; y < textureSize; y++) {
				colors[x + y * textureSize] = texture[x, y];
			}
		}

		Texture2D t2d = new Texture2D(textureSize, textureSize);
		t2d.SetPixels(colors);
		t2d.Apply();
		return t2d;
	}
}

public enum LayerBlendMode {
	Normal,
	Multiply,
	Screen
}