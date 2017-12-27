using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public void PlayGame() {
		SceneManager.LoadScene ("TetrisGame", LoadSceneMode.Single);
	}

	public void Quit() {
		Application.Quit();
	}
}
