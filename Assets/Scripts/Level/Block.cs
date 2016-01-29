using UnityEngine;
using System.Collections;

public class Block {
	//Base block constructor
	public Block() {

	}

	/// <summary>
	/// Adds the blocks to a meshdata 
	/// </summary>
	/// <param name="chunk">Chunk that the block is in</param>
	/// <param name="x">X position of the block</param>
	/// <param name="y">Y position of the block</param>
	/// <param name="z">Z position of the block</param>
	/// <param name="data">The meshdata to add to</param>
	/// <returns>Meshdata with added block meshdata</returns>
	public virtual MeshData BlockData(Chunk chunk, int x, int y, int z, MeshData data) {
		data.useRenderDataForCol = true;

		if (!chunk.GetBlock(x, y + 1, z).IsSolid(Direction.Down)) {
			data = FaceDataUp(chunk, x, y, z, data);
		}

		if (!chunk.GetBlock(x, y - 1, z).IsSolid(Direction.Up)) {
			data = FaceDataDown(chunk, x, y, z, data);
		}

		if (!chunk.GetBlock(x, y, z + 1).IsSolid(Direction.South)) {
			data = FaceDataNorth(chunk, x, y, z, data);
		}

		if (!chunk.GetBlock(x, y, z - 1).IsSolid(Direction.North)) {
			data = FaceDataSouth(chunk, x, y, z, data);
		}

		if (!chunk.GetBlock(x + 1, y, z).IsSolid(Direction.West)) {
			data = FaceDataEast(chunk, x, y, z, data);
		}

		if (!chunk.GetBlock(x - 1, y, z).IsSolid(Direction.East)) {
			data = FaceDataWest(chunk, x, y, z, data);
		}

		return data;
	}

	protected virtual MeshData FaceDataUp(Chunk chunk, int x, int y, int z, MeshData data) {
		data.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
		data.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
		data.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
		data.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
		data.AddQuadTriangles();

		return data;
	}

	protected virtual MeshData FaceDataDown(Chunk chunk, int x, int y, int z, MeshData data) {
		data.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
		data.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
		data.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
		data.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
		data.AddQuadTriangles();

		return data;
	}

	protected virtual MeshData FaceDataNorth(Chunk chunk, int x, int y, int z, MeshData data) {
		data.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
		data.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
		data.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
		data.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
		data.AddQuadTriangles();

		return data;
	}

	protected virtual MeshData FaceDataEast(Chunk chunk, int x, int y, int z, MeshData data) {
		data.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
		data.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
		data.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
		data.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
		data.AddQuadTriangles();

		return data;
	}

	protected virtual MeshData FaceDataSouth(Chunk chunk, int x, int y, int z, MeshData data) {
		data.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
		data.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
		data.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
		data.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
		data.AddQuadTriangles();

		return data;
	}

	protected virtual MeshData FaceDataWest(Chunk chunk, int x, int y, int z, MeshData data) {
		data.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
		data.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
		data.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
		data.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
		data.AddQuadTriangles();

		return data;
	}

	/// <summary>
	/// Gets the solidity of a blocks face
	/// </summary>
	public virtual bool IsSolid(Direction direction) {
		switch (direction) {
			case Direction.North:
				return true;
			case Direction.South:
				return true;
			case Direction.East:
				return true;
			case Direction.West:
				return true;
			case Direction.Up:
				return true;
			case Direction.Down:
				return true;
		}

		return false;
	}
}

public enum Direction {
	North,
	East,
	South,
	West,
	Up,
	Down
}