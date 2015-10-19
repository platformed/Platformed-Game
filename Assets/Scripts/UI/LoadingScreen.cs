using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour {
	public Text loadingText;

	void Start() {
		StartCoroutine(LoadLevel(UIManager.getScene()));
	}

	IEnumerator LoadLevel(string level) {
		AsyncOperation async = Application.LoadLevelAsync(level);
		while (!async.isDone) {
			//transform.localScale = new Vector3(async.progress, 1);
			loadingText.text = "LOADING " + Mathf.Round(async.progress * 100) + "%";
			yield return null;
		}
	}
}
