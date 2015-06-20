using UnityEngine;
using System.Collections;

public class manager : MonoBehaviour {
	public void changeScene(string scene){
		Application.LoadLevel (scene);
	}

	public void exit(){
		Application.Quit ();
	}
}
