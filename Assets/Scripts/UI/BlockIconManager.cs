using UnityEngine;
using System.Collections;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BlockIconManager : MonoBehaviour {
#if UNITY_EDITOR
	public Camera iconCamera;
	public RenderTexture iconTexture;
	public Transform parent;

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

		data.Rotate(Quaternion.Euler(0, -90, 0));
		
		//Instantiate if it is a spawnable block
		if (block is SpawnableBlock) {
			SpawnableBlock b = (SpawnableBlock)block;
			b.InstantiateBlock(transform, Vector3.zero, 0, 0, 0, new Block[,,] { { { } } });

			//Add verticies to meshdata for scaling
			MeshFilter[] meshs = b.GetPrefab().GetComponentsInChildren<MeshFilter>();
			foreach(MeshFilter m in meshs) {
				data.AddVertices(m.sharedMesh.vertices, m.sharedMesh.normals, m.transform.localPosition, m.transform.rotation);
			}

			//Set layer
			foreach (Transform child in transform) {
				child.gameObject.layer = 8;
			}
		}

		//Scale
		ScaleBlock(block, data);

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

	void ScaleBlock(Block block, MeshData data) {
		//Position so it's in the center
		float minX = float.MaxValue;
		float maxX = float.MinValue;

		float minY = float.MaxValue;
		float maxY = float.MinValue;

		float minZ = float.MaxValue;
		float maxZ = float.MinValue;

		foreach (Vector3 v in data.vertices) {
			if (v.x < minX) {
				minX = v.x;
			} else if (v.x > maxX) {
				maxX = v.x;
			}

			if (v.y < minY) {
				minY = v.y;
			} else if (v.y > maxY) {
				maxY = v.y;
			}

			if (v.z < minZ) {
				minZ = v.z;
			} else if (v.z > maxZ) {
				maxZ = v.z;
			}
		}

		transform.position = new Vector3((minX + maxX) / -2f, (minY + maxY) / -2f, (minZ + maxZ) / -2f);

		//Find scale
		float maxDistance = float.MinValue;
		if (maxX + transform.position.x > maxDistance) {
			maxDistance = maxX + transform.position.x;
		}
		if (maxY + transform.position.y > maxDistance) {
			maxDistance = maxY + transform.position.y;
		}
		if (maxZ + transform.position.y > maxDistance) {
			maxDistance = maxZ + transform.position.z;
		}

		float scale = 1f / maxDistance;

		//Scale the parent so the position isn't affected
		parent.localScale = new Vector3(scale, scale, scale);
	}

	void RenderIcon(Block block) {
		iconCamera.Render();
		RenderTexture.active = iconTexture;

		Texture2D icon = new Texture2D(iconTexture.width, iconTexture.height);
		icon.alphaIsTransparency = true;
		icon.ReadPixels(new Rect(0, 0, iconTexture.width, iconTexture.height), 0, 0);
		icon.Apply();

		File.WriteAllBytes(Application.dataPath + "/Resources/Block Icons/" + block.GetName() + ".png", icon.EncodeToPNG());
		
		block.DestroyBlock();
	}
#endif
}
