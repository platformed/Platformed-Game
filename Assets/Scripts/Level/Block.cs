using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Represents a block in the game
/// </summary>
public class Block {
	protected string name;
	protected string displayName;
	protected BlockType blockType = BlockType.Block;
	protected Rect uv;
	public int textureID;

	byte rotation = 0;

	//Base block constructor
	public Block() {

	}

	public Block Copy() {
		return (Block) MemberwiseClone();
	}

	/// <summary>
	/// Gets the name of the block used for internal purposes
	/// </summary>
	/// <returns>Name</returns>
	public string GetName() {
		return name;
	}

	/// <summary>
	/// Gets the name of the block used for display purposes
	/// </summary>
	/// <returns>Name</returns>
	public string GetDisplayName() {
		return displayName;
	}

	/// <summary>
	/// Gets the mesh for the custom model
	/// </summary>
	/// <returns>Mesh</returns>
	public Mesh GetCustomModel() {
		return (Mesh) Resources.Load("Blocks/" + GetName() + "/" + GetName() + "Model", typeof(Mesh));
	}

	/// <summary>
	/// Adds the blocks to a meshdata 
	/// </summary>
	/// <param name="chunk">Chunk that the block is in</param>
	/// <param name="x">X position of the block</param>
	/// <param name="y">Y position of the block</param>
	/// <param name="z">Z position of the block</param>
	/// <param name="data">The meshdata to add to</param>
	/// <param name="submesh">The submesh to put the block on</param>
	/// <param name="ignoreChunk">If the solidity of the neighboring blocks should be checked</param>
	/// <returns>Meshdata with added block meshdata</returns>
	public virtual MeshData BlockData(Chunk chunk, int x, int y, int z, MeshData data, int submesh, bool ignoreChunk) {
		data.useRenderDataForCol = true;

		//Use custom model if it exists
		if (blockType == BlockType.Model) {
			Mesh mesh = GetCustomModel();

			//Add the verticies, triangles, and uvs
			data.AddTriangles(mesh.triangles, submesh);
			data.AddVertices(mesh.vertices, mesh.normals, new Vector3(x, y - 0.5f, z), Quaternion.Euler(-90, 90 * rotation, 0));
			data.AddUVs(mesh.uv);

			return data;
		}

		Vector3[] v = new Vector3[8];

		if (blockType == BlockType.Block) {
			v[0] = new Vector3(-0.5f, -0.5f, -0.5f);
			v[1] = new Vector3(-0.5f, -0.5f, 0.5f);
			v[2] = new Vector3(-0.5f, 0.5f, -0.5f);
			v[3] = new Vector3(-0.5f, 0.5f, 0.5f);
			v[4] = new Vector3(0.5f, -0.5f, -0.5f);
			v[5] = new Vector3(0.5f, -0.5f, 0.5f);
			v[6] = new Vector3(0.5f, 0.5f, -0.5f);
			v[7] = new Vector3(0.5f, 0.5f, 0.5f);
		} else {
			v[0] = new Vector3(-0.5f, -0.5f, -0.5f);
			v[1] = new Vector3(-0.5f, -0.5f, 0.5f);
			v[2] = new Vector3(-0.5f, -0.4f, -0.5f);
			v[3] = new Vector3(-0.5f, -0.4f, 0.5f);
			v[4] = new Vector3(0.5f, -0.5f, -0.5f);
			v[5] = new Vector3(0.5f, -0.5f, 0.5f);
			v[6] = new Vector3(0.5f, -0.4f, -0.5f);
			v[7] = new Vector3(0.5f, -0.4f, 0.5f);
		}

		//Add cube verticies
		if (ignoreChunk || !chunk.GetBlock(x, y + 1, z).IsSolid(Direction.Down)) {
			data.AddVertex(v[3] + new Vector3(x, y, z), Vector3.up);
			data.AddVertex(v[7] + new Vector3(x, y, z), Vector3.up);
			data.AddVertex(v[6] + new Vector3(x, y, z), Vector3.up);
			data.AddVertex(v[2] + new Vector3(x, y, z), Vector3.up);
			data.AddQuadTriangles(submesh);
			data.AddUVs(FaceUVs(Direction.Up));
		}

		if (ignoreChunk || !chunk.GetBlock(x, y - 1, z).IsSolid(Direction.Up)) {
			data.AddVertex(v[0] + new Vector3(x, y, z), Vector3.down);
			data.AddVertex(v[4] + new Vector3(x, y, z), Vector3.down);
			data.AddVertex(v[5] + new Vector3(x, y, z), Vector3.down);
			data.AddVertex(v[1] + new Vector3(x, y, z), Vector3.down);
			data.AddQuadTriangles(submesh);
			data.AddUVs(FaceUVs(Direction.Down));
		}

		if (ignoreChunk || !chunk.GetBlock(x, y, z + 1).IsSolid(Direction.South)) {
			data.AddVertex(v[5] + new Vector3(x, y, z), Vector3.forward);
			data.AddVertex(v[7] + new Vector3(x, y, z), Vector3.forward);
			data.AddVertex(v[3] + new Vector3(x, y, z), Vector3.forward);
			data.AddVertex(v[1] + new Vector3(x, y, z), Vector3.forward);
			data.AddQuadTriangles(submesh);
			data.AddUVs(FaceUVs(Direction.North));
		}

		if (ignoreChunk || !chunk.GetBlock(x, y, z - 1).IsSolid(Direction.North)) {
			data.AddVertex(v[0] + new Vector3(x, y, z), Vector3.back);
			data.AddVertex(v[2] + new Vector3(x, y, z), Vector3.back);
			data.AddVertex(v[6] + new Vector3(x, y, z), Vector3.back);
			data.AddVertex(v[4] + new Vector3(x, y, z), Vector3.back);
			data.AddQuadTriangles(submesh);
			data.AddUVs(FaceUVs(Direction.South));
		}

		if (ignoreChunk || !chunk.GetBlock(x + 1, y, z).IsSolid(Direction.West)) {
			data.AddVertex(v[4] + new Vector3(x, y, z), Vector3.right);
			data.AddVertex(v[6] + new Vector3(x, y, z), Vector3.right);
			data.AddVertex(v[7] + new Vector3(x, y, z), Vector3.right);
			data.AddVertex(v[5] + new Vector3(x, y, z), Vector3.right);
			data.AddQuadTriangles(submesh);
			data.AddUVs(FaceUVs(Direction.East));
		}

		if (ignoreChunk || !chunk.GetBlock(x - 1, y, z).IsSolid(Direction.East)) {
			data.AddVertex(v[1] + new Vector3(x, y, z), Vector3.left);
			data.AddVertex(v[3] + new Vector3(x, y, z), Vector3.left);
			data.AddVertex(v[2] + new Vector3(x, y, z), Vector3.left);
			data.AddVertex(v[0] + new Vector3(x, y, z), Vector3.left);
			data.AddQuadTriangles(submesh);
			data.AddUVs(FaceUVs(Direction.West));
		}

		return data;
	}
	
	public virtual Vector2[] FaceUVs(Direction direction) {
		//Rotate the top and bottom face of the block
		if(direction == Direction.Up || direction == Direction.Down) {
			Vector2[] uvs = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 0), new Vector2(0, 1) };

			Vector2[] rotatedUvs = new Vector2[4];
            for (int i = 0; i < 4; i++) {
				rotatedUvs[i] = uvs[(i + 4 - rotation) % 4];
			}

			return rotatedUvs;
		}

		return new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 0), new Vector2(0, 1) };

		/*
		I'm currently using a texture atlas for the textures,
		generated every time you start the game, but because of
		rounding errors, you will get seams on the textures. I will
		fix this by switching to array textures when Unity 5.4 gets
		released on March 16th, 2016.
		*/


		/*Vector2[] uvs = new Vector2[4];

		//TODO: Get better way of finding uv coords
		int textureIndex = BlockManager.GetBlocks().FindIndex(x => x.GetName() == name) - 1;
		Rect rect = TextureManager.uvs[textureIndex];*/

		//Half pixel correction
		/*rect.x += (1f / TextureManager.atlasWidth);
		rect.y += (1f / TextureManager.atlasHeight);
		rect.width -= (2f / TextureManager.atlasWidth);
		rect.height -= (2f / TextureManager.atlasHeight);*/

		/*uvs[0] = new Vector2(rect.x + rect.width, rect.y);				//1, 0
		uvs[1] = new Vector2(rect.x + rect.width, rect.y + rect.height);	//1, 1
		uvs[2] = new Vector2(rect.x, rect.y + rect.height);					//0, 1
		uvs[3] = new Vector2(rect.x, rect.y);								//0, 0
		
		return uvs;*/
	}

	/// <summary>
	/// Gets the solidity of a blocks face
	/// </summary>
	public virtual bool IsSolid(Direction direction) {
		if (blockType == BlockType.Block) {
			return true;
		} else {
			return false;
		}
	}

	/// <summary>
	/// Sets thg the block's rotation by 90 degree increments on the y axis
	/// </summary>
	/// <param name="rot">The value to rotate to</param>
	public void SetRotation(byte rot) {
		//Add the rotation
		rotation = rot;

		//Loop the rotation around 4
		rotation %= 4;
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

public enum BlockType {
	Block,
	Floor,
	Model
}