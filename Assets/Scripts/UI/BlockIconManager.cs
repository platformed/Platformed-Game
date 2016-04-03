using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

public class BlockIconManager : MonoBehaviour {
#if UNITY_EDITOR
	public Camera iconCamera;
	public RenderTexture iconTexture;

	MeshFilter filter;
	MeshRenderer meshRenderer;

	bool rendered = false;

	void Start() {
		filter = GetComponent<MeshFilter>();
		meshRenderer = GetComponent<MeshRenderer>();
	}

	void Update() {
		if (!rendered) {
			RenderBlockIcons();
			rendered = true;
		}
	}

	public void RenderBlockIcons() {
		foreach (Block b in BlockManager.GetBlocks()) {
			if (b.GetName() != "Air") {
				RenderBlock(b);
				RenderIcon(b);
			}
		}

		AssetDatabase.Refresh();
	}

	void RenderBlock(Block block) {
		MeshData data = new MeshData();
		block.BlockData(0, 0, 0, data, 0, new Block[,,] { { { block } } });

		//Clear mesh
		filter.mesh.Clear();

		//Verticies
		filter.mesh.vertices = data.vertices.ToArray();

		//Submeshes
		filter.mesh.subMeshCount = data.triangles.Count;
		for (int i = 0; i < data.triangles.Count; i++) {
			filter.mesh.SetTriangles(data.triangles[i], i);
		}

		//UVs and normals
		filter.mesh.uv = data.uvs.ToArray();
		filter.mesh.normals = data.normals.ToArray();
		
		//Material
		meshRenderer.material = Resources.Load("Blocks/" + block.GetName() + "/" + block.GetName() + "Material") as Material;
	}

	void RenderIcon(Block block) {
		iconCamera.Render();
		RenderTexture.active = iconTexture;

		Texture2D icon = new Texture2D(iconTexture.width, iconTexture.height);
		icon.alphaIsTransparency = true;
		icon.ReadPixels(new Rect(0, 0, iconTexture.width, iconTexture.height), 0, 0);
		icon.Apply();

		File.WriteAllBytes(Application.dataPath + "/Resources/Block Icons/" + block.GetName() + ".png", icon.EncodeToPNG());
	}
#endif
}
