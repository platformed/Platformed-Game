using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {
	public Text loadingText;

	void Start() {
		StartCoroutine(LoadLevel(UIManager.getScene()));
	}

	IEnumerator LoadLevel(string level) {
		AsyncOperation async = SceneManager.LoadSceneAsync(level);
		while (!async.isDone) {
			//transform.localScale = new Vector3(async.progress, 1);
			//loadingText.text = "LOADING " + Mathf.Round(async.progress * 100) + "%";
			yield return null;
		}
	}
}
