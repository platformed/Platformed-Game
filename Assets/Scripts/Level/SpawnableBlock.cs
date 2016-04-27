using UnityEngine;
using System.Collections;

public class SpawnableBlock : Block {
	protected GameObject gameobject;
	protected Transform transform;
	protected Vector3 blockPosition;

	public override MeshData BlockData(int x, int y, int z, MeshData data, int submesh, Block[,,] blocks) {
		return data;
	}

	public override BlockSolidity GetSolidity(Direction direction) {
		return BlockSolidity.None;
	}

	public void InstantiateBlock(Vector3 pos) {
		blockPosition = pos;

		GameObject prefab = (GameObject)Resources.Load("Blocks/" + GetName() + "/" + GetName() + "Prefab", typeof(GameObject)); //Resources.Load("Blocks/" + GetName() + "/" + GetName() + "Prefab") as GameObject;
		gameobject = Object.Instantiate(prefab, blockPosition, Quaternion.Euler(-90, 0, 0)) as GameObject;

		transform = gameobject.transform;
		transform.SetParent(UIManager.world.transform);

		SpawnableController controller = gameobject.AddComponent<SpawnableController>();
		controller.SetBlock(this);
    }

	public void DestroyBlock() {
		Object.Destroy(gameobject);
	}

	public virtual void InactiveStart() {

	}

	public virtual void InactiveUpdate() {

	}

	public virtual void Start() {

	}

	public virtual void Update() {
		
	}
}
