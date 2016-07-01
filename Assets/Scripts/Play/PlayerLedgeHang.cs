using UnityEngine;
using System.Collections;

public class PlayerLedgeHang : MonoBehaviour {
	[HideInInspector]
	public bool isHanging = false;

	bool hasWall;

	void Update() {
		if (CanHang()) {
			isHanging = true;

		}
	}

	/// <summary>
	/// Returns true if the player can hang on a ledge
	/// </summary>
	/// <returns>If the player can hang on a ledge</returns>
	bool CanHang() {
		//Checks for a wall near the player
		if (Physics.CheckSphere(transform.position, 0.3f)) {
			
		}
		
		return false;
	}
}
