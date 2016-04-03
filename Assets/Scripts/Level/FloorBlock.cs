using UnityEngine;
using System.Collections;

public class FloorBlock : Block {
	public override MeshData BlockData(int x, int y, int z, MeshData data, int submesh, Block[,,] blocks) {
		data.useRenderDataForCol = true;

		Vector3[] v = new Vector3[8];

		v[0] = new Vector3(-0.5f, -0.5f, -0.5f);
		v[1] = new Vector3(-0.5f, -0.5f, 0.5f);
		v[2] = new Vector3(-0.5f, -0.4f, -0.5f);
		v[3] = new Vector3(-0.5f, -0.4f, 0.5f);
		v[4] = new Vector3(0.5f, -0.5f, -0.5f);
		v[5] = new Vector3(0.5f, -0.5f, 0.5f);
		v[6] = new Vector3(0.5f, -0.4f, -0.5f);
		v[7] = new Vector3(0.5f, -0.4f, 0.5f);

		//Add floor verticies
		if (CheckSolid(blocks, x, y + 1, z, Direction.Down)) {
			data.AddVertex(v[3] + new Vector3(x, y, z), Vector3.up);
			data.AddVertex(v[7] + new Vector3(x, y, z), Vector3.up);
			data.AddVertex(v[6] + new Vector3(x, y, z), Vector3.up);
			data.AddVertex(v[2] + new Vector3(x, y, z), Vector3.up);
			data.AddQuadTriangles(submesh);
			data.AddUVs(FaceUVs(Direction.Up));
		}

		if (CheckSolid(blocks, x, y - 1, z, Direction.Up)) {
			data.AddVertex(v[0] + new Vector3(x, y, z), Vector3.down);
			data.AddVertex(v[4] + new Vector3(x, y, z), Vector3.down);
			data.AddVertex(v[5] + new Vector3(x, y, z), Vector3.down);
			data.AddVertex(v[1] + new Vector3(x, y, z), Vector3.down);
			data.AddQuadTriangles(submesh);
			data.AddUVs(FaceUVs(Direction.Down));
		}

		if (CheckSolid(blocks, x, y, z + 1, Direction.South)) {
			data.AddVertex(v[5] + new Vector3(x, y, z), Vector3.forward);
			data.AddVertex(v[7] + new Vector3(x, y, z), Vector3.forward);
			data.AddVertex(v[3] + new Vector3(x, y, z), Vector3.forward);
			data.AddVertex(v[1] + new Vector3(x, y, z), Vector3.forward);
			data.AddQuadTriangles(submesh);
			data.AddUVs(FaceUVs(Direction.North));
		}

		if (CheckSolid(blocks, x, y, z - 1, Direction.North)) {
			data.AddVertex(v[0] + new Vector3(x, y, z), Vector3.back);
			data.AddVertex(v[2] + new Vector3(x, y, z), Vector3.back);
			data.AddVertex(v[6] + new Vector3(x, y, z), Vector3.back);
			data.AddVertex(v[4] + new Vector3(x, y, z), Vector3.back);
			data.AddQuadTriangles(submesh);
			data.AddUVs(FaceUVs(Direction.South));
		}

		if (CheckSolid(blocks, x + 1, y, z, Direction.West)) {
			data.AddVertex(v[4] + new Vector3(x, y, z), Vector3.right);
			data.AddVertex(v[6] + new Vector3(x, y, z), Vector3.right);
			data.AddVertex(v[7] + new Vector3(x, y, z), Vector3.right);
			data.AddVertex(v[5] + new Vector3(x, y, z), Vector3.right);
			data.AddQuadTriangles(submesh);
			data.AddUVs(FaceUVs(Direction.East));
		}

		if (CheckSolid(blocks, x - 1, y, z, Direction.East)) {
			data.AddVertex(v[1] + new Vector3(x, y, z), Vector3.left);
			data.AddVertex(v[3] + new Vector3(x, y, z), Vector3.left);
			data.AddVertex(v[2] + new Vector3(x, y, z), Vector3.left);
			data.AddVertex(v[0] + new Vector3(x, y, z), Vector3.left);
			data.AddQuadTriangles(submesh);
			data.AddUVs(FaceUVs(Direction.West));
		}

		return data;
	}

	protected override bool CheckSolid(Block[,,] blocks, int x, int y, int z, Direction direction) {
		if (x < 0 || x > blocks.GetLength(0) - 1) {
			return true;
		}
		if (y < 0 || y > blocks.GetLength(1) - 1) {
			return true;
		}
		if (z < 0 || z > blocks.GetLength(2) - 1) {
			return true;
		}

		return !(blocks[x, y, z].GetSolidity(direction) == BlockSolidity.Block || blocks[x, y, z].GetSolidity(direction) == BlockSolidity.Floor);
	}

	public override BlockSolidity GetSolidity(Direction direction) {
		if (direction == Direction.Up) {
			return BlockSolidity.None;
		} else if (direction == Direction.Down) {
			return BlockSolidity.Block;
		} else {
			return BlockSolidity.Floor;
		}
	}
}
