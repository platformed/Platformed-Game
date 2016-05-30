using UnityEngine;
using System.Collections;

public class TorchBlock : SpawnableBlock {
	int lit;
	[Property("Lit", "If the torch is lit", "0:1:1")]
	public int Lit {
		get {
			return lit;
		}
		set {
			lit = value;
			if (Spawned) {
				light.SetActive(lit == 1 ? true : false);
				particleSystem.SetActive(lit == 1 ? true : false);
			}
		}
	}

	GameObject light;
	GameObject particleSystem;

	public TorchBlock() {
		Name = "Torch";
		DisplayName = "Torch";

		Lit = 1;
	}

	public override void Spawn() {
		light = transform.GetComponentInChildren<Light>().gameObject;
		particleSystem = transform.GetComponentInChildren<ParticleSystem>().gameObject;
		Lit = Lit;
	}
}
