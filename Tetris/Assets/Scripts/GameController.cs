using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static float timerConst = 0.7f;
	public int level = 1;
	public int score = 0;
	public int lines = 0;
	public Text scoreText;
	public Text levelText;
	public Text linesText;
	public GameObject gameOverScreen;
	public GameObject pausePanel;
    public Image soundButtonImage;
    public Sprite muteSprite;
    public Sprite soundSprite;
 	void Start () {
		timerConst = 0.7f;
		gameOverScreen.SetActive (false);
		pausePanel.SetActive (false);
		AudioListener.volume = 100;
        soundButtonImage.sprite = soundSprite;
        FindObjectOfType<Spawner>().SpawnFirstShape ();
		FindObjectOfType<NextShape> ().NextRandomShape ();
	}

	public void UpdateResults(int numOfFullRows) {
		lines += numOfFullRows;
		int maxScore = score + Points (numOfFullRows, level);
		StopCoroutine ("UpdateScore");
		StartCoroutine("UpdateScore", maxScore);
		if (lines > 10 * level) {
			StartCoroutine ("UpdateLevel");
			timerConst -= 0.1f;
		}
		linesText.text = lines.ToString ();
		levelText.text = level.ToString();
	}

	IEnumerator UpdateScore(int maxScore) {
		float duration = 0.7f;
		int start = score;
		for (float timer = 0; timer <= duration; timer += Time.deltaTime) {
			float progress = timer / duration;
			score = (int)Mathf.Lerp (start, maxScore, progress);
			scoreText.text = score.ToString ();
			yield return null;
		}
		score = maxScore;
		scoreText.text = score.ToString ();
	}

	IEnumerator UpdateLevel() {
		float duration = 0.4f;
		int start = levelText.fontSize;
		Color32 startColor = levelText.color;
		Color32 endColor = new Color32 (0, 130, 255, 255);
		for (float timer = 0; timer <= duration; timer += Time.deltaTime) {
			float progress = timer / duration;
			levelText.fontSize = (int)Mathf.Lerp (start, 3*start, progress);
			levelText.color = Color32.Lerp (startColor, endColor, progress);
			yield return null;
		}
		level++;
		levelText.text = level.ToString ();
		for (float timer = 0; timer <= duration; timer += Time.deltaTime) {
			float progress = timer / duration;
			levelText.fontSize = (int)Mathf.Lerp (3*start, start, progress);
			levelText.color = Color32.Lerp (endColor, startColor, progress);
			yield return null;
		}
		levelText.fontSize = start;
		levelText.color = startColor;
	}

	static int Points(int numOfFullRows, int lvl) {
		switch (numOfFullRows) {
			case 0:
				return 0;
			case 1:
				return 40 * lvl;
			case 2:
				return 100 * lvl;
			case 3:
				return 300 * lvl;
			default:
				return 1200 * lvl;
		}
	}

	public void ChangeSoundState() {
		if (AudioListener.volume > 0)
        {
            AudioListener.volume = 0;
            soundButtonImage.sprite = muteSprite;
        }
        else
        {
            AudioListener.volume = 100;
            soundButtonImage.sprite = soundSprite;
        }
	}

	public void PauseGame() {
		Time.timeScale = 0f;
		pausePanel.SetActive (true);
	}
	public void ResumeGame() {
		Time.timeScale = 1f;
		pausePanel.SetActive (false);
	}
	public void EndGame() {
		gameOverScreen.SetActive (true);
	}

	public void RestartGame() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
	public void Quit() {
		Application.Quit();
	}
}
