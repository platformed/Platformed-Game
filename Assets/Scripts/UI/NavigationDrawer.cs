using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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

	void Start() {
		anim = navDrawer.GetComponent<Animator>();

		aboutDialogPrefab = Resources.Load("UI/Dialog/About Dialog") as GameObject;
		settingsDialogPrefab = Resources.Load("UI/Dialog/Settings Dialog") as GameObject;
	}

	void Update() {
		if (shadow != null) {
			shadow.color = new Color(0, 0, 0, Mathf.Lerp(shadow.color.a, shadowValue, Time.deltaTime * 10));
		}
	}

	/// <summary>
	/// Shows the navigation drawer
	/// </summary>
	public void ShowDrawer() {
		anim.Play("NavDrawerSlideIn");
		shadowValue = 0.5f;
		rightShadow.SetActive(true);

		if (shadow != null) {
			shadow.GetComponent<CanvasGroup>().blocksRaycasts = true;
		}
		UIManager.instance.navDrawerEnabled = true;
	}

	/// <summary>
	/// Hides the navigation drawer
	/// </summary>
	public void HideDrawer() {
		anim.Play("NavDrawerSlideOut");
		shadowValue = 0f;
		rightShadow.SetActive(false);

		if (shadow != null) {
			shadow.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
		UIManager.instance.navDrawerEnabled = false;
	}

	/// <summary>
	/// Loads a scene
	/// </summary>
	/// <param name="s">The name of the scene to load</param>
	void LoadScene(string scene) {
		SceneManager.LoadSceneAsync(scene);
	}

	/// <summary>
	/// Home button
	/// </summary>
	public void Home() {
		LoadScene("main-menu");
	}

	/// <summary>
	/// Design button
	/// </summary>
	public void Design() {
		HideDrawer();
		LoadScene("level");
	}

	/// <summary>
	/// Play button
	/// </summary>
	public void Play() {
		HideDrawer();
		LoadScene("level-browser");
	}

	/// <summary>
	/// Quit button
	/// </summary>
	public void Quit() {
		Application.Quit();
	}

	/// <summary>
	/// Opens the settings dialog
	/// </summary>
	public void Settings() {
		if (settingsDialog == null) {
			settingsDialog = Instantiate(settingsDialogPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f), Quaternion.identity) as GameObject;
			settingsDialog.transform.SetParent(windowCanvas);
			settingsDialog.transform.SetAsLastSibling();
		}
	}

	/// <summary>
	/// Opens the about dialog
	/// </summary>
	public void About() {
		if (aboutDialog == null) {
			aboutDialog = Instantiate(aboutDialogPrefab, new Vector3(Screen.width / 2f, Screen.height / 2f), Quaternion.identity) as GameObject;
			aboutDialog.transform.SetParent(windowCanvas);
			aboutDialog.transform.SetAsLastSibling();
		}
	}
}
