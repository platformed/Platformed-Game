using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class BlockManager : EditorWindow {
	string blockName = "";
	string displayName = "";

	[MenuItem("Window/Block Manager")]
	public static void ShowWindow() {
		GetWindow(typeof(BlockManager));
	}

	void OnGUI() {
		GUILayout.Label("Add Block", EditorStyles.boldLabel);
		
		GUILayout.Label("Please ensure that the block name has no spaces and the first letter of every word is capitalized.\n", EditorStyles.wordWrappedLabel);
		blockName = EditorGUILayout.TextField("Block Name", blockName);
		displayName = EditorGUILayout.TextField("Display Name", displayName);

		if (GUILayout.Button("Add Block")) {
			addBlock();
		}
	}

	void addBlock() {
		string copyPath = "Assets/Resources/Blocks/" + blockName + "/" + blockName + "Block.cs";
		Debug.Log("Creating file: " + copyPath);

		//Don't overwrite
		if (!File.Exists(copyPath)) {
			using (StreamWriter outfile =
				new StreamWriter(copyPath)) {
				outfile.WriteLine("public class " + blockName + "Block : BlockType {");
					outfile.WriteLine("\tpublic " + blockName + "Block() {");
						outfile.WriteLine("\t\tname = \"" + blockName + "\";");
						outfile.WriteLine("\t\tdisplayName = \"" + blockName + "\";");
				//TODO: check if block has model
						outfile.WriteLine("\t\tisCube = true;");
					outfile.WriteLine("\t}");
				outfile.WriteLine("}");
			}
		}

		//Refresh
		AssetDatabase.Refresh();
	}
}