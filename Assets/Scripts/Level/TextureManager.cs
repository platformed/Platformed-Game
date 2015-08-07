using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TextureManager : MonoBehaviour {
	List<Texture2D> textures = new List<Texture2D>();
	public static Rect[] uvs;
	public static int atlasWidth;
	public static int atlasHeight;

	void Start () {
		foreach(Block b in Block.blocks) {
			string tex = b.getTexture();
			if(tex != null) {
				textures.Add(Resources.Load("Blocks/" + tex) as Texture2D);
			}
		}

		Texture2D atlas = new Texture2D (0, 0);
		uvs = atlas.PackTextures (textures.ToArray(), 16, 16384);

		atlasWidth = atlas.width;
		atlasHeight = atlas.height;

		byte[] bytes = atlas.EncodeToPNG ();
		File.WriteAllBytes (Application.dataPath + "/Materials/blockAtlas.png", bytes);
	}

	void Update () {
	
	}
}
