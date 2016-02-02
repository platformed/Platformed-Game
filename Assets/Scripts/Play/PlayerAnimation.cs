using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	Animator anim;
	public Rigidbody rb;
	
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void Update () {
		Vector3 velocity = rb.velocity;

		//If the player is moving 
        if (velocity.x > 0.1f || velocity.x < -0.1f || velocity.z > 0.1f || velocity.z < -0.1f) {
			anim.SetBool("Walking", true);
		} else {
			anim.SetBool("Walking", false);
		}
	}
}
