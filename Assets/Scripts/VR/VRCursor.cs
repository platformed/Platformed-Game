﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VRCursor : MonoBehaviour {
	float smoothPos = 30f;
	//float smoothRot = 20f;
	public World world;
	public Transform controllerTransform;
	public Canvas menuCanvas;
	public Collider menuCollider;
	public MeshRenderer lineRenderer;
	public Transform lineTransform;

	public SteamVR_TrackedObject trackedObject;
	SteamVR_Controller.Device controller;

	Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;

	static Block[,,] block;

	MeshRenderer meshRenderer;
	MeshFilter filter;

	List<string> blockTypes = new List<string>();

	//If the cursor should be updated at the end of the frame
	static bool update = true;

	void Start() {
		block = new Block[,,] { { { new BricksBlock() } } };
		meshRenderer = GetComponent<MeshRenderer>();
		filter = GetComponent<MeshFilter>();
	}

	void Update() {
		if (controller == null) {
			if ((int)trackedObject.index != -1) {
				controller = SteamVR_Controller.Input((int)trackedObject.index);
			} else {
				return;
			}
		}

		Vector3 pos = controllerTransform.position;

		pos = new Vector3(Mathf.Floor(pos.x), Mathf.Floor(pos.y), Mathf.Floor(pos.z)) + new Vector3(0.5f, 0.5f, 0.5f);

		RaycastHit hit;
		if (menuCollider.Raycast(new Ray(controllerTransform.position, controllerTransform.forward), out hit, 100f)) {
			if (menuCanvas.gameObject.activeInHierarchy) {
				lineRenderer.enabled = true;
				lineTransform.localScale = new Vector3(lineTransform.localScale.x, hit.distance / 32f, lineTransform.localScale.z);
				lineTransform.localPosition = new Vector3(0, 0, hit.distance / 32f);

				RaycastHit buttonHit;
				if (Physics.Raycast(new Ray(controllerTransform.position, controllerTransform.forward), out buttonHit, 100f) && controller.GetPressDown(triggerButton)) {
					VRBlockButton button = buttonHit.transform.GetComponent<VRBlockButton>();
					if (button != null) {
						button.Click();
						Debug.Log("clicked");
					}
				}
			}
		} else {
			lineRenderer.enabled = false;

			if (controller.GetPress(triggerButton)) {
				for (int x = 0; x < block.GetLength(0); x++) {
					for (int y = 0; y < block.GetLength(1); y++) {
						for (int z = 0; z < block.GetLength(2); z++) {
							if (block[x, y, z].GetName() != "Air") {
								world.SetBlock((int)(pos.x + x), (int)(pos.y + y), (int)(pos.z + z), block[x, y, z].Copy());
							}
						}
					}
				}
			}

			if (controller.GetPress(gripButton)) {
				world.SetBlock((int)pos.x, (int)pos.y, (int)pos.z, new AirBlock());
			}
		}

		transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * smoothPos);
		//transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * smoothRot);

		if (update) {
			RenderCursor();
			update = false;
		}
	}

	public static void SetBlock(Block[,,] b) {
		block = b;
		update = true;
	}

	public void Copy(Block[,,] blocks, Vector3 offset) {
		block = blocks;
		BlockCursor.offset = offset;

		update = true;
	}

	void RenderCursor() {
		MeshData data = new MeshData();

		blockTypes.Clear();
		for (int x = 0; x < block.GetLength(0); x++) {
			for (int y = 0; y < block.GetLength(1); y++) {
				for (int z = 0; z < block.GetLength(2); z++) {
					if (block[x, y, z].GetName() != "Air") {
						//Try to find if the block type already exists in chunk
						int submesh = blockTypes.IndexOf(block[x, y, z].GetName());

						//If there are no blocks with its type, add it
						if (submesh == -1) {
							blockTypes.Add(block[x, y, z].GetName());
							submesh = blockTypes.Count - 1;
						}

						data = block[x, y, z].BlockData(x, y, z, data, submesh, block);
					}
				}
			}
		}

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

		//Materials
		Material[] materials = new Material[filter.mesh.subMeshCount];
		for (int i = 0; i < materials.Length; i++) {
			materials[i] = BlockManager.GetBlock(blockTypes[i]).GetMaterial();
		}
		meshRenderer.materials = materials;
	}
}
