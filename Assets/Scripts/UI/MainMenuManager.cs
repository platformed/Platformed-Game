using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {
	public void designMode(){
		UIManager.LoadScene("level");
	}

	public void playMode(){
		
	}

	public void settings(){
		
	}

	public void exit(){
		Application.Quit ();
	}
}
