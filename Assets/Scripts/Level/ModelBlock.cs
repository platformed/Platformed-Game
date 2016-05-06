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

	public override BlockSolidity GetSolidity(Direction direction) {
		return BlockSolidity.None;
	}
}
