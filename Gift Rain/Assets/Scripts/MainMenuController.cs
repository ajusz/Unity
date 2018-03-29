using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public AudioSource clickSound;
	public void Play() {
		clickSound.Play ();
		StartCoroutine ("PlayCoroutine");
	}

	public void Quit() {
		clickSound.Play();
		StartCoroutine ("QuitCoroutine");
	}

	IEnumerator PlayCoroutine() {
		yield return new WaitForSecondsRealtime (0.2f);
		SceneManager.LoadScene ("Gift Rain", LoadSceneMode.Single);
	}

	IEnumerator QuitCoroutine() {
		yield return new WaitForSecondsRealtime (0.2f);
		Application.Quit();
	}
}
