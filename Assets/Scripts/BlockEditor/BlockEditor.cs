using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BlockEditor : MonoBehaviour {
	public static int textureSize = 1024;
	public static int textureWidth = textureSize;
	public static int textureHeight = textureSize;

	List<BlockLayer> layers = new List<BlockLayer>();
	public Material material;

	bool update;

	Texture2D texture;

	void Start() {
		texture = new Texture2D(textureWidth, textureHeight);

		layers.Add(new BlockLayer(LayerBlendMode.Normal));

		update = true;
	}

	void FixedUpdate() {
		if (Input.GetMouseButton(0)) {
			//Get world position
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			//Center
			pos += new Vector3(0.5f, 0.5f, 0f);

			//Scale
			pos *= textureSize;

			//Temp
			//layers[0].DrawSquare((int) pos.x, (int) pos.y, 20, Color.white);
			Color[] c = new Color[20 * 20];
			for (int i = 0; i < 20 * 20; i++) {
				c[i] = Color.white;
			}
			texture.SetPixels((int)pos.x, (int)pos.y, 20, 20, c);
			update = true;
		}

		if (Input.GetMouseButtonUp(0)) {
			//update = true;
		}

		if (update) {
			//Render();
			texture.Apply();
			material.mainTexture = texture;
			update = false;
		}
	}

	void Update() {
		//Set camera viewport rect
		int h = Screen.height;
		int w = Screen.width;
		int right = 300;
		int top = 64;
		Camera.main.rect = new Rect(0, 0, (w - right) / (float)w, (h - top) / (float)h);
	}

	void Render() {
		Color[,] tex = new Color[textureSize, textureSize];

		List<Color[,]> layerColors = new List<Color[,]>();
		List<LayerBlendMode> layerBlendModes = new List<LayerBlendMode>();

		foreach (BlockLayer l in layers) {
			layerColors.Add(l.Render());
			layerBlendModes.Add(l.blendMode);
		}

		for (int i = 0; i < layerColors.Count; i++) {
			for (int x = 0; x < textureSize; x++) {
				for (int y = 0; y < textureSize; y++) {
					tex[x, y] = layerColors[i][x, y];
				}
			}
		}

		Color[] colors = new Color[textureSize * textureSize];

		for (int x = 0; x < textureSize; x++) {
			for (int y = 0; y < textureSize; y++) {
				colors[x + y * textureSize] = tex[x, y];
			}
		}
		
		texture.SetPixels(colors);
		texture.Apply();
	}
}

public enum LayerBlendMode {
	Normal,
	Multiply,
	Screen
}

/*byte[] bytes = new byte[textureWidth * textureHeight * 4];
		for (int x = 0; x < textureWidth; x++) {
			for (int y = 0; y < textureHeight; y++) {
				Color c = tex[x, y];
                bytes[(x + y * textureWidth) * 4] = Convert.ToByte(c.r == 1 ? 255 : c.r * 256);
				bytes[(x + y * textureWidth) * 4 + 1] = Convert.ToByte(c.g == 1 ? 255 : c.g * 256);
				bytes[(x + y * textureWidth) * 4 + 2] = Convert.ToByte(c.b == 1 ? 255 : c.b * 256);
				bytes[(x + y * textureWidth) * 4 + 3] = Convert.ToByte(c.a == 1 ? 255 : c.a * 256);
			}
		}

		texture.LoadRawTextureData(bytes);*/
