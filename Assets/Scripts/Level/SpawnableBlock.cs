using UnityEngine;
using System.Collections;

public class SpawnableBlock : Block {
	public GameObject gameobject;
	public Transform transform;
	protected Vector3 blockPosition;

	public override MeshData BlockData(int x, int y, int z, MeshData data, int submesh, Block[,,] blocks) {
		return data;
	}

	public override BlockSolidity GetSolidity(Direction direction) {
		return BlockSolidity.None;
	}

	public GameObject GetPrefab() {
		return Resources.Load("Blocks/" + GetName() + "/" + GetName() + "Prefab") as GameObject;
	}

	public void InstantiateBlock(Transform parent, Vector3 pos) {
		blockPosition = pos;

		gameobject = Object.Instantiate(GetPrefab(), blockPosition, Quaternion.Euler(-90, 0, 0)) as GameObject;
		transform = gameobject.transform;

		transform.SetParent(parent);
		transform.localPosition = blockPosition;

		SpawnableController controller = gameobject.AddComponent<SpawnableController>();
		controller.SetBlock(this);
    }

	public void DestroyBlock() {
		Object.Destroy(gameobject);
	}

	public virtual void Reset() {

	}

	public virtual void InactiveUpdate() {

	}

	public virtual void Update() {
		
	}

	public virtual void OnPlayerCollision() {

	}
}
