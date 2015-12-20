using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NavigationDrawer : MonoBehaviour {
	public GameObject navDrawer;
	public GameObject rightShadow;
	public Image shadow;
	float shadowValue;

	private Animator anim;

	void Start () {
		anim = navDrawer.GetComponent<Animator>();
	}
	
	void Update () {
		if (shadow != null) {
			shadow.color = new Color(0, 0, 0, Mathf.Lerp(shadow.color.a, shadowValue, Time.deltaTime * 10));
		}
	}

	public void showDrawer() {
		anim.Play("NavDrawerSlideIn");
		shadowValue = 0.5f;
		rightShadow.SetActive(true);
	}

	public void hideDrawer() {
		anim.Play("NavDrawerSlideOut");
		shadowValue = 0f;
		rightShadow.SetActive(false);
	}

	void loadScene(string s) {
		UIManager.loadScene(s);
		//hideDrawer();
	}

	public void home() {
		hideDrawer();
		loadScene("main-menu");
	}

	public void design() {
		hideDrawer();
		loadScene("level");
	}

	public void play() {
		hideDrawer();
		loadScene("level-browser");
	}

	public void quit() {
		Application.Quit();
	}

	public void settings() {

	}

	public void about() {

	}
}
