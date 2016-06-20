using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Represents a block in the game
/// </summary>
public class Block {
	public GameObject gameObject { get; protected set; }

	protected string Name { get; set; }
	protected string DisplayName { get; set; }
	public int textureID;

	protected byte rotation = 0;

	public Block Copy() {
		return (Block)MemberwiseClone();
	}

	/// <summary>
	/// Gets the name of the block used for internal purposes
	/// </summary>
	/// <returns>Name</returns>
	public string GetName() {
		return Name;
	}

	/// <summary>
	/// Gets the name of the block used for display purposes
	/// </summary>
	/// <returns>Name</returns>
	public string GetDisplayName() {
		return DisplayName;
	}

	/// <summary>
	/// Gets the mesh for the custom model
	/// </summary>
	/// <returns>Mesh</returns>
	public Mesh GetCustomModel() {
		return (Mesh)Resources.Load("Blocks/" + GetName() + "/" + GetName() + "Model", typeof(Mesh));
	}

	public virtual Material GetMaterial() {
		return Resources.Load("Blocks/" + Name + "/" + Name + "Material") as Material;
	}

	/// <summary>
	/// Adds the blocks to a meshdata 
	/// </summary>
	/// <param name="x">X position of the block</param>
	/// <param name="y">Y position of the block</param>
	/// <param name="z">Z position of the block</param>
	/// <param name="data">The meshdata to add to</param>
	/// <param name="submesh">The submesh to put the block on</param>
	/// <param name="ignoreChunk">If the solidity of the neighboring blocks should be checked</param>
	/// <returns>Meshdata with added block meshdata</returns>
	public virtual MeshData BlockData(int x, int y, int z, MeshData data, int submesh, Block[,,] blocks) {
		data.useRenderDataForCol = true;

		Vector3[] v = new Vector3[8];

		v[0] = new Vector3(-0.5f, -0.5f, -0.5f);
		v[1] = new Vector3(-0.5f, -0.5f, 0.5f);
		v[2] = new Vector3(-0.5f, 0.5f, -0.5f);
		v[3] = new Vector3(-0.5f, 0.5f, 0.5f);
		v[4] = new Vector3(0.5f, -0.5f, -0.5f);
		v[5] = new Vector3(0.5f, -0.5f, 0.5f);
		v[6] = new Vector3(0.5f, 0.5f, -0.5f);
		v[7] = new Vector3(0.5f, 0.5f, 0.5f);

		//Add cube verticies

		//Top
		if (!CheckSolid(blocks, x, y + 1, z, Direction.Down)) {
			byte[] ao = CalculateFaceAO(new bool[] {
				CheckSolid(blocks, x - 1, y + 1, z    , Direction.East),
				CheckSolid(blocks, x - 1, y + 1, z + 1, Direction.East),
				CheckSolid(blocks, x    , y + 1, z + 1, Direction.South),
				CheckSolid(blocks, x + 1, y + 1, z + 1, Direction.South),
				CheckSolid(blocks, x + 1, y + 1, z    , Direction.West),
				CheckSolid(blocks, x + 1, y + 1, z - 1, Direction.West),
				CheckSolid(blocks, x    , y + 1, z - 1, Direction.North),
				CheckSolid(blocks, x - 1, y + 1, z - 1, Direction.North)
			});
			data.AddVertex(v[3] + new Vector3(x, y, z), Vector3.up, CalculateVertexColor(ao[0]));
			data.AddVertex(v[7] + new Vector3(x, y, z), Vector3.up, CalculateVertexColor(ao[1]));
			data.AddVertex(v[6] + new Vector3(x, y, z), Vector3.up, CalculateVertexColor(ao[2]));
			data.AddVertex(v[2] + new Vector3(x, y, z), Vector3.up, CalculateVertexColor(ao[3]));
			data.AddQuadTriangles(submesh, FlipQuad(ao));
			data.AddUVs(FaceUVs(Direction.Up));
		}

		//Bottom
		if (!CheckSolid(blocks, x, y - 1, z, Direction.Up)) {
			byte[] ao = CalculateFaceAO(new bool[] {
				CheckSolid(blocks, x - 1, y - 1, z    , Direction.East),
				CheckSolid(blocks, x - 1, y - 1, z - 1, Direction.East),
				CheckSolid(blocks, x    , y - 1, z - 1, Direction.North),
				CheckSolid(blocks, x + 1, y - 1, z - 1, Direction.North),
				CheckSolid(blocks, x + 1, y - 1, z    , Direction.West),
				CheckSolid(blocks, x + 1, y - 1, z + 1, Direction.West),
				CheckSolid(blocks, x    , y - 1, z + 1, Direction.South),
				CheckSolid(blocks, x - 1, y - 1, z + 1, Direction.South)
			});
			data.AddVertex(v[0] + new Vector3(x, y, z), Vector3.down, CalculateVertexColor(ao[0]));
			data.AddVertex(v[4] + new Vector3(x, y, z), Vector3.down, CalculateVertexColor(ao[1]));
			data.AddVertex(v[5] + new Vector3(x, y, z), Vector3.down, CalculateVertexColor(ao[2]));
			data.AddVertex(v[1] + new Vector3(x, y, z), Vector3.down, CalculateVertexColor(ao[3]));
			data.AddQuadTriangles(submesh, FlipQuad(ao));
			data.AddUVs(FaceUVs(Direction.Down));
		}

		//North
		if (!CheckSolid(blocks, x, y, z + 1, Direction.South)) {
			byte[] ao = CalculateFaceAO(new bool[] {
				CheckSolid(blocks, x    , y - 1, z + 1, Direction.Up),
				CheckSolid(blocks, x + 1, y - 1, z + 1, Direction.Up),
				CheckSolid(blocks, x + 1, y    , z + 1, Direction.East),
				CheckSolid(blocks, x + 1, y + 1, z + 1, Direction.East),
				CheckSolid(blocks, x    , y + 1, z + 1, Direction.Down),
				CheckSolid(blocks, x - 1, y + 1, z + 1, Direction.Down),
				CheckSolid(blocks, x - 1, y    , z + 1, Direction.West),
				CheckSolid(blocks, x - 1, y - 1, z + 1, Direction.West)
			});
			data.AddVertex(v[5] + new Vector3(x, y, z), Vector3.forward, CalculateVertexColor(ao[0]));
			data.AddVertex(v[7] + new Vector3(x, y, z), Vector3.forward, CalculateVertexColor(ao[1]));
			data.AddVertex(v[3] + new Vector3(x, y, z), Vector3.forward, CalculateVertexColor(ao[2]));
			data.AddVertex(v[1] + new Vector3(x, y, z), Vector3.forward, CalculateVertexColor(ao[3]));
			data.AddQuadTriangles(submesh, FlipQuad(ao));
			data.AddUVs(FaceUVs(Direction.North));
		}

		//South
		if (!CheckSolid(blocks, x, y, z - 1, Direction.North)) {
			byte[] ao = CalculateFaceAO(new bool[] {
				CheckSolid(blocks, x    , y - 1, z - 1, Direction.Up),
				CheckSolid(blocks, x - 1, y - 1, z - 1, Direction.Up),
				CheckSolid(blocks, x - 1, y    , z - 1, Direction.East),
				CheckSolid(blocks, x - 1, y + 1, z - 1, Direction.East),
				CheckSolid(blocks, x    , y + 1, z - 1, Direction.Down),
				CheckSolid(blocks, x + 1, y + 1, z - 1, Direction.Down),
				CheckSolid(blocks, x + 1, y    , z - 1, Direction.West),
				CheckSolid(blocks, x + 1, y - 1, z - 1, Direction.West)
			});
			data.AddVertex(v[0] + new Vector3(x, y, z), Vector3.back, CalculateVertexColor(ao[0]));
			data.AddVertex(v[2] + new Vector3(x, y, z), Vector3.back, CalculateVertexColor(ao[1]));
			data.AddVertex(v[6] + new Vector3(x, y, z), Vector3.back, CalculateVertexColor(ao[2]));
			data.AddVertex(v[4] + new Vector3(x, y, z), Vector3.back, CalculateVertexColor(ao[3]));
			data.AddQuadTriangles(submesh, FlipQuad(ao));
			data.AddUVs(FaceUVs(Direction.South));
		}

		//East
		if (!CheckSolid(blocks, x + 1, y, z, Direction.West)) {
			byte[] ao = CalculateFaceAO(new bool[] {
				CheckSolid(blocks, x + 1, y - 1, z    , Direction.Up),
				CheckSolid(blocks, x + 1, y - 1, z - 1, Direction.Up),
				CheckSolid(blocks, x + 1, y    , z - 1, Direction.North),
				CheckSolid(blocks, x + 1, y + 1, z - 1, Direction.North),
				CheckSolid(blocks, x + 1, y + 1, z    , Direction.Down),
				CheckSolid(blocks, x + 1, y + 1, z + 1, Direction.Down),
				CheckSolid(blocks, x + 1, y    , z + 1, Direction.South),
				CheckSolid(blocks, x + 1, y - 1, z + 1, Direction.South)
			});
			data.AddVertex(v[4] + new Vector3(x, y, z), Vector3.right, CalculateVertexColor(ao[0]));
			data.AddVertex(v[6] + new Vector3(x, y, z), Vector3.right, CalculateVertexColor(ao[1]));
			data.AddVertex(v[7] + new Vector3(x, y, z), Vector3.right, CalculateVertexColor(ao[2]));
			data.AddVertex(v[5] + new Vector3(x, y, z), Vector3.right, CalculateVertexColor(ao[3]));
			data.AddQuadTriangles(submesh, FlipQuad(ao));
			data.AddUVs(FaceUVs(Direction.East));
		}

		//West
		if (!CheckSolid(blocks, x - 1, y, z, Direction.East)) {
			byte[] ao = CalculateFaceAO(new bool[] {
				CheckSolid(blocks, x - 1, y - 1, z    , Direction.Up),
				CheckSolid(blocks, x - 1, y - 1, z + 1, Direction.Up),
				CheckSolid(blocks, x - 1, y    , z + 1, Direction.North),
				CheckSolid(blocks, x - 1, y + 1, z + 1, Direction.North),
				CheckSolid(blocks, x - 1, y + 1, z    , Direction.Down),
				CheckSolid(blocks, x - 1, y + 1, z - 1, Direction.Down),
				CheckSolid(blocks, x - 1, y    , z - 1, Direction.South),
				CheckSolid(blocks, x - 1, y - 1, z - 1, Direction.South)
			});
			data.AddVertex(v[1] + new Vector3(x, y, z), Vector3.left, CalculateVertexColor(ao[0]));
			data.AddVertex(v[3] + new Vector3(x, y, z), Vector3.left, CalculateVertexColor(ao[1]));
			data.AddVertex(v[2] + new Vector3(x, y, z), Vector3.left, CalculateVertexColor(ao[2]));
			data.AddVertex(v[0] + new Vector3(x, y, z), Vector3.left, CalculateVertexColor(ao[3]));
			data.AddQuadTriangles(submesh, FlipQuad(ao));
			data.AddUVs(FaceUVs(Direction.West));
		}

		return data;
	}

	byte[] CalculateFaceAO(bool[] surroundingBlocks) {
		return new byte[] {
			CalculateVertexAO(surroundingBlocks[0], surroundingBlocks[1], surroundingBlocks[2]),
			CalculateVertexAO(surroundingBlocks[2], surroundingBlocks[3], surroundingBlocks[4]),
			CalculateVertexAO(surroundingBlocks[4], surroundingBlocks[5], surroundingBlocks[6]),
			CalculateVertexAO(surroundingBlocks[6], surroundingBlocks[7], surroundingBlocks[0])
		};
	}

	byte CalculateVertexAO(bool side1, bool corner, bool side2) {
		if (side1 && side2) {
			return 0;
		}
		return (byte)(3 - ((side1 ? 1 : 0) + (side2 ? 1 : 0) + (corner ? 1 : 0)));
	}

	Color CalculateVertexColor(byte ao) {
		//Range from 0.5 to 1
		return Color.white * ((ao / 6f) + 0.5f);
	}

	bool FlipQuad(byte[] ao) {
		return ao[0] + ao[2] > ao[1] + ao[3];
	}

	/// <summary>
	/// Checks if a blocks face is solid in a ceratian direction from a block array
	/// </summary>
	/// <param name="blocks">Block array to check from</param>
	/// <param name="x">X position of block</param>
	/// <param name="y">Y position of block</param>
	/// <param name="z">Z position of block</param>
	/// <param name="direction">Direction to check</param>
	/// <returns>True if the block is not solid</returns>
	protected virtual bool CheckSolid(Block[,,] blocks, int x, int y, int z, Direction direction) {
		if (x < 0 || x > blocks.GetLength(0) - 1) {
			return false;
		}
		if (y < 0 || y > blocks.GetLength(1) - 1) {
			return false;
		}
		if (z < 0 || z > blocks.GetLength(2) - 1) {
			return false;
		}

		return blocks[x, y, z].GetSolidity(direction) == BlockSolidity.Block;
	}

	public virtual Vector2[] FaceUVs(Direction direction) {
		//Rotate the top and bottom face of the block
		if (direction == Direction.Up || direction == Direction.Down) {
			Vector2[] uvs = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 0), new Vector2(0, 1) };

			Vector2[] rotatedUvs = new Vector2[4];
			for (int i = 0; i < 4; i++) {
				rotatedUvs[i] = uvs[(i + 4 - rotation) % 4];
			}

			return rotatedUvs;
		}

		return new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };

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
	public virtual BlockSolidity GetSolidity(Direction direction) {
		return BlockSolidity.Block;
	}

	/// <summary>
	/// Add a collider to the chunk
	/// </summary>
	/// <param name="parent">Chunk to add to</param>
	/// <param name="pos">Center of collider</param>
	/// <returns>Collider for block</returns>
	public virtual Collider GetCollider(GameObject parent, Vector3 pos) {
		BoxCollider box = parent.AddComponent<BoxCollider>();
		box.center = pos;
		box.size = Vector3.one;
		return box;
	}

	/// <summary>
	/// Instantiates block gameobject
	/// </summary>
	/// <param name="parent">Parent of the gameobject</param>
	/// <param name="pos">Position of the gameobject</param>
	/// <param name="x">X position of the block in the block array</param>
	/// <param name="y">Y position of the block in the block array</param>
	/// <param name="z">Z position of the block in the block array</param>
	/// <param name="blocks">Block array to calculate visibility</param>
	public virtual void InstantiateBlock(Transform parent, Vector3 pos, int x, int y, int z, Block[,,] blocks) {
		gameObject = new GameObject(Name);
		gameObject.transform.SetParent(parent);
		gameObject.transform.position = pos;

		gameObject.AddComponent<MeshFilter>();

		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.sharedMaterial = GetMaterial();
		meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;

		UpdateBlock(x, y, z, blocks);

		GetCollider(gameObject, Vector3.zero);
	}


	/// <summary>
	/// Destroys block gameobject
	/// </summary>
	public virtual void DestroyBlock() {
		UnityEngine.Object.Destroy(gameObject);
	}

	/// <summary>
	/// Updates the mesh of the block
	/// </summary>
	/// <param name="x">X position of the block in the block array</param>
	/// <param name="y">Y position of the block in the block array</param>
	/// <param name="z">Z position of the block in the block array</param>
	/// <param name="blocks">Block array to calculate visibility</param>
	public virtual void UpdateBlock(int x, int y, int z, Block[,,] blocks) {
		gameObject.GetComponent<MeshRenderer>().enabled = true;

		MeshData data = BlockData(x, y, z, new MeshData(), 0, blocks);
		data.Offset(-new Vector3(x, y, z));

		MeshFilter filter = gameObject.GetComponent<MeshFilter>();
		filter.mesh.Clear();
		if (data.triangles.Count > 0) {
			filter.mesh.vertices = data.vertices.ToArray();
			filter.mesh.triangles = data.triangles[0].ToArray();
			filter.mesh.uv = data.uvs.ToArray();
			filter.mesh.normals = data.normals.ToArray();
			filter.mesh.colors = data.colors.ToArray();
		} else {
			gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}

	/// <summary>
	/// Changes the block's rotation by 90 degree increments on the y axis
	/// </summary>
	/// <param name="rot">The amount to rotate</param>
	public void Rotate(byte rot) {
		//Add the rotation
		rotation += rot;

		//Loop the rotation around 4
		rotation %= 4;
	}

	/// <summary>
	/// Gets the rotation of the block
	/// </summary>
	/// <returns>Rotation of the block</returns>
	public byte GetRotation() {
		return rotation;
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

public enum BlockSolidity {
	Block,
	Floor,
	None
}