using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEditor;

public class TexturePacker : EditorWindow {
	/*List<Texture2D> textures = new List<Texture2D>();
	public static Rect[] uvs;
	public static int atlasWidth;
	public static int atlasHeight;*/

	[MenuItem("Editor/Pack Textures")]
	public static void ShowWindow() {
		PackTextures();
	}

	void OnGUI() {
		if (GUILayout.Button("Pack Textures")) {
			PackTextures();
		}
	}

	static void PackTextures() {
		/*
		foreach (BlockType b in Block.getBlocks()) {
			string tex = b.getName();
			if (tex != null) {
				textures.Add(Resources.Load("Blocks/" + tex + "/" + tex) as Texture2D);
			}
		}

		Texture2D atlas = new Texture2D(0, 0);
		uvs = atlas.PackTextures(textures.ToArray(), 0, 16384);

		atlasWidth = atlas.width;
		atlasHeight = atlas.height;
		
		byte[] bytes = atlas.EncodeToPNG();
		File.WriteAllBytes(Application.dataPath + "/Materials/blockAtlas.png", bytes);
		*/

		Debug.Log("Packed Textures");
	}
}
