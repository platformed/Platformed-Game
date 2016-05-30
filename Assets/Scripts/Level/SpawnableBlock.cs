using UnityEngine;
using System.Collections;

public class SpawnableBlock : Block {
	public GameObject gameObject;
	public Transform transform;
	protected Vector3 blockPosition;

	public override MeshData BlockData(int x, int y, int z, MeshData data, int submesh, Block[,,] blocks) {
		return data;
	}

	public override BlockSolidity GetSolidity(Direction direction) {
		return BlockSolidity.None;
	}

	public override Collider GetCollider(GameObject parent, Vector3 pos) {
		//Get mesh filters
		MeshFilter[] meshFilters = transform.GetComponentsInChildren<MeshFilter>();
		CombineInstance[] meshes = new CombineInstance[meshFilters.Length];

		//Add meshes to array
		for (int i = 0; i < meshes.Length; i++) {
			meshes[i].mesh = meshFilters[i].sharedMesh;
			meshes[i].transform = meshFilters[i].transform.localToWorldMatrix;
		}

		//Combine meshes
		Mesh mesh = new Mesh();
		mesh.CombineMeshes(meshes);

		//Add position
		Vector3[] verticies = mesh.vertices;
		for (int i = 0; i < verticies.Length; i++) {
			verticies[i] += -blockPosition + new Vector3(0f, -0.5f, 0f) + pos;
		}
		mesh.vertices = verticies;

		//Create mesh collider
		MeshCollider coll = parent.AddComponent<MeshCollider>();
		coll.sharedMesh = mesh;
		coll.convex = true;
		coll.isTrigger = true;

		return coll;
	}

	/// <summary>
	/// Gets the prefab of this block
	/// </summary>
	/// <returns>The prefab of this block</returns>
	public GameObject GetPrefab() {
		return Resources.Load("Blocks/" + GetName() + "/" + GetName() + "Prefab") as GameObject;
	}

	/// <summary>
	/// Spawn the GameObject of the blocks prefab
	/// </summary>
	/// <param name="parent">Parent of GameObject</param>
	/// <param name="pos">Local position to spawn at</param>
	public override void InstantiateBlock(Transform parent, Vector3 pos, int x, int y, int z, Block[,,] blocks) {
		blockPosition = pos;

		//Instantiate block
		gameObject = Object.Instantiate(GetPrefab(), blockPosition, Quaternion.Euler(-90, 0, 0)) as GameObject;
		transform = gameObject.transform;

		//Set parent and position
		transform.SetParent(parent);
		transform.localPosition = blockPosition;

		//Create spawnable controller
		SpawnableController controller = gameObject.AddComponent<SpawnableController>();
		controller.SetBlock(this);
    }

	/// <summary>
	/// Destroys the GameObject for the block
	/// </summary>
	public override void DestroyBlock() {
		Object.Destroy(gameObject);
	}

	/// <summary>
	/// Called when the block is placed or the gamemode switches
	/// Use to reset the blocks position
	/// </summary>
	public virtual void Reset() {

	}

	/// <summary>
	/// Called every frame during design mode
	/// </summary>
	public virtual void InactiveUpdate() {

	}

	/// <summary>
	/// Called every frame during play mode
	/// </summary>
	public virtual void Update() {
		
	}

	/// <summary>
	/// Called on the frame the player touches the parent object's collider
	/// </summary>
	public virtual void OnPlayerEnter() {

	}
}
