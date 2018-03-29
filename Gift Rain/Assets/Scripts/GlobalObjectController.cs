using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObjectController : MonoBehaviour {

	public static GlobalObjectController Instance;
	public int highScore = 0;

	void Start() {
		highScore = GlobalObjectController.Instance.highScore;
	}
	void Awake () {
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
	}
	public void SavePlayer() {
		GlobalObjectController.Instance.highScore = highScore;
	}
}
