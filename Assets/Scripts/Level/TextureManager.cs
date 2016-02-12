using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEditor;

/*
I'm currently using a texture atlas for the textures,
generated every time you start the game, but because of
rounding errors, you will get seams on the textures. I will
fix this by switching to array textures when Unity 5.4 gets
released on March 16th, 2016.
*/

public class TextureManager : MonoBehaviour {
	/*static List<Texture2D> textures = new List<Texture2D>();

	public static Rect[] uvs;
	public static int atlasWidth;
	public static int atlasHeight;

	void Start() {
		PackTextures();
	}

	/// <summary>
	/// Packs the block textures into a texture atlas
	/// </summary>
	static void PackTextures() {
		List<Block> blocks = BlockManager.GetBlocks();

		int i = 0;
		foreach (Block b in blocks) {
			string name = b.GetName();

			//Try to load the texture
			Texture2D texture = Resources.Load("Blocks/" + name + "/" + name) as Texture2D;

			//If it could be found, add it
            if (texture != null) {
				textures.Add(texture);
				b.textureID = i;
				i++;
			}
		}

		Texture2D atlas = new Texture2D(0, 0);
		uvs = atlas.PackTextures(textures.ToArray(), 0, 16384);

		atlasWidth = atlas.width;
		atlasHeight = atlas.height;
		
		//Write the texture to a file
		byte[] bytes = atlas.EncodeToPNG();
		File.WriteAllBytes(Application.dataPath + "/Materials/Block Atlas.png", bytes);

		//Refresh the block atlas texture
		AssetDatabase.Refresh();
	}*/
}
