﻿using UnityEngine;
using System.Collections;

public class CollectableBlock : SpawnableBlock {
	protected int points;

	public override void Reset() {
		transform.rotation = Quaternion.Euler(-90, 0, 0);
		transform.localPosition = blockPosition;

		gameObject.GetComponent<MeshRenderer>().enabled = true;
	}

	public override void Update() {
		transform.rotation = Quaternion.Euler(-90f, Time.time * 40f, 0f);
		transform.position = blockPosition + new Vector3(0f, Mathf.Sin(Time.time * 2f) * 0.05f, 0f);
	}

	public override void OnPlayerEnter() {
		gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
}