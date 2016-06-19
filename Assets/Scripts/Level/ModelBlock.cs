using UnityEngine;
using System.Collections;

public class ModelBlock : Block {
	public override MeshData BlockData(int x, int y, int z, MeshData data, int submesh, Block[,,] blocks) {
		data.useRenderDataForCol = true;

		Mesh mesh = GetCustomModel();

		//Add the verticies, triangles, and uvs
		data.AddTriangles(mesh.triangles, submesh);
		data.AddVertices(mesh.vertices, mesh.normals, new Vector3(x, y - 0.5f, z), Quaternion.Euler(-90, 90 * rotation, 0));
		data.AddUVs(mesh.uv);

		return data;
	}

	public override void InstantiateBlock(Transform parent, Vector3 pos, int x, int y, int z, Block[,,] blocks) {
		base.InstantiateBlock(parent, pos, x, y, z, blocks);

		//Remove old box collider
		Object.Destroy(gameObject.GetComponent<Collider>());

		//Add mesh collider
		MeshCollider collider = gameObject.AddComponent<MeshCollider>();
	}

	public override Collider GetCollider(GameObject parent, Vector3 pos) {
		//Add mesh collider
		MeshCollider collider = parent.AddComponent<MeshCollider>();
		//Set the mesh to mesh on the MeshFilter on the block's gameobject
		collider.sharedMesh = gameObject.GetComponent<MeshFilter>().mesh;

		return collider;
	}

	public override BlockSolidity GetSolidity(Direction direction) {
		return BlockSolidity.None;
	}
}
