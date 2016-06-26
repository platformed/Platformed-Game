using UnityEngine;
using System.Collections;

public class SpawnableBlock : Block {
	public Transform transform;

	/// <summary>
	/// True if the block has been spawned
	/// </summary>
	protected bool Spawned {
		get {
			return transform != null;
		}
	}

	public override MeshData BlockData(int x, int y, int z, MeshData data, int submesh, Block[,,] blocks) {
		return data;
	}

	public override void UpdateBlock(int x, int y, int z, Block[,,] blocks) { }

	public override BlockSolidity GetSolidity(Direction direction) {
		return BlockSolidity.None;
	}

	public override Collider GetCollider(GameObject parent, Vector3 pos) {
		//Get mesh filters
		MeshFilter[] meshFilters = GetPrefab().GetComponentsInChildren<MeshFilter>();
		CombineInstance[] meshes = new CombineInstance[meshFilters.Length];

		//Add meshes to array
		for (int i = 0; i < meshes.Length; i++) {
			meshes[i].mesh = meshFilters[i].sharedMesh;
			meshes[i].transform = meshFilters[i].transform.localToWorldMatrix;
		}

		//Combine meshes
		Mesh mesh = new Mesh();
		mesh.CombineMeshes(meshes);

		//Add position and rotate
		Vector3[] verticies = mesh.vertices;
		for (int i = 0; i < verticies.Length; i++) {
			verticies[i] += pos;
			verticies[i] = Quaternion.Euler(90, 0, 0) * verticies[i];
		}
		mesh.vertices = verticies;

		//Create mesh collider
		MeshCollider coll = parent.AddComponent<MeshCollider>();
		coll.sharedMesh = mesh;
		
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
		//Create parent gameobject to store position of block
		Transform posOffset = new GameObject(GetName()).transform;
		posOffset.SetParent(parent);
		posOffset.localPosition = pos + new Vector3(0f, -0.5f, 0f);

		//Instantiate block
		gameObject = Object.Instantiate(GetPrefab()) as GameObject;
		transform = gameObject.transform;

		//Set parent
		transform.SetParent(posOffset);
		transform.localPosition = Vector3.zero;

		//Create spawnable controller
		SpawnableController controller = gameObject.AddComponent<SpawnableController>();
		controller.SetBlock(this);

		Spawn();
	}

	/// <summary>
	/// Destroys the GameObject for the block
	/// </summary>
	public override void DestroyBlock() {
		Object.Destroy(transform.parent.gameObject);
	}

	/// <summary>
	/// Called when the block is placed for the first time
	/// </summary>
	public virtual void Spawn() {

	}

	/// <summary>
	/// <para>Called when the block is placed or the gamemode switches</para>
	/// <para>Use to reset the blocks position</para>
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
