using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NavigationDrawer : MonoBehaviour {
	public GameObject navDrawer;
	public GameObject rightShadow;
	public Transform windowCanvas;
	public Image shadow;
	float shadowValue;

	private Animator anim;

	GameObject aboutDialogPrefab;
	GameObject settingsDialogPrefab;

	GameObject aboutDialog;
	GameObject settingsDialog;

	void Start () {
		anim = navDrawer.GetComponent<Animator>();

		aboutDialogPrefab = Resources.Load("UI/Dialog/About Dialog") as GameObject;
		settingsDialogPrefab = Resources.Load("UI/Dialog/Settings Dialog") as GameObject;
	}
	
	void Update () {
		if (shadow != null) {
			shadow.color = new Color(0, 0, 0, Mathf.Lerp(shadow.color.a, shadowValue, Time.deltaTime * 10));
		}
	}

	public void ShowDrawer() {
		anim.Play("NavDrawerSlideIn");
		shadowValue = 0.5f;
		rightShadow.SetActive(true);

		if (shadow != null) {
			shadow.GetComponent<CanvasGroup>().blocksRaycasts = true;
		}
		UIManager.NavDrawerEnabled(true);
	}

	public void HideDrawer() {
		anim.Play("NavDrawerSlideOut");
		shadowValue = 0f;
		rightShadow.SetActive(false);

		if (shadow != null) {
			shadow.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
		UIManager.NavDrawerEnabled(false);
	}

	void LoadScene(string s) {
		UIManager.loadScene(s);
		//hideDrawer();
	}

	public void Home() {
		HideDrawer();
		LoadScene("main-menu");
	}

	public void Design() {
		HideDrawer();
		LoadScene("level");
	}

	public void Play() {
		HideDrawer();
		LoadScene("level-browser");
	}

	public void Quit() {
		Application.Quit();
	}

	public void Settings() {
		if (settingsDialog == null) {
			settingsDialog = Instantiate(settingsDialogPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f), Quaternion.identity) as GameObject;
			settingsDialog.transform.SetParent(windowCanvas);
			settingsDialog.transform.SetAsLastSibling();
		}
	}

	public void About() {
		if (aboutDialog == null) {
			aboutDialog = Instantiate(aboutDialogPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f), Quaternion.identity) as GameObject;
			aboutDialog.transform.SetParent(windowCanvas);
			aboutDialog.transform.SetAsLastSibling();
		}
	}
}
