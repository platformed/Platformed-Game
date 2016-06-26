using UnityEngine;
using System.Collections;

public class FallingSticksBlock : SpawnableBlock {
	[Property("Hold time", "How long it will stay before it starts moving in seconds", "0:10:0.1")]
	public float HoldTime { get; set; }

	[Property("Fall speed", "The speed that it will fall in blocks per second", "0:10:0.5")]
	public float FallSpeed { get; set; }

	[Property("Respawn time", "How long it will take before it resets", "0:60:1")]
	public float RespawnTime { get; set; }

	bool falling = false;
	float time = 0f;

	public FallingSticksBlock() {
		Name = "FallingSticks";
		DisplayName = "Falling Sticks";
		FallSpeed = 1f;
		HoldTime = 1f;
		RespawnTime = 10f;
	}

	public override void OnPlayerEnter() {
		falling = true;
		time = 0f;
	}

	public override void Reset() {
		transform.localPosition = Vector3.zero;
		falling = false;
	}

	public override void Update() {
		time += Time.deltaTime;

		if (falling && time >= HoldTime) {
			transform.Translate(new Vector3(0f, 0, -FallSpeed * Time.deltaTime));
		}

		if(time >= RespawnTime) {
			Reset();
		}
	}
}
