using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	int score;
	public Text scoreText;
	public Text highScoreText;
	int level;
	public Text levelText;
	public int lives;
	public Text livesText;
	public int livesTextSize;
	public Color32 livesTextColor;
	public GameObject gameOverScreen;
	public GameObject pausePanel;
	float speedUpTimeInterval;
	float lastTime;
	public float giftSpeed;
	public float fireballSpeed;
	public AudioSource clickSound;
	// Use this for initialization
	void Start () {
		lastTime = Time.time;
		gameOverScreen.SetActive (false);
		pausePanel.SetActive (false);
		score = 0;
		scoreText.text = score.ToString ();
		highScoreText.text = GlobalObjectController.Instance.highScore.ToString ();
		lives = 1;
		livesText.text = lives.ToString ();
		level = 1;
		levelText.text = level.ToString ();
		giftSpeed = 2;
		fireballSpeed = 3.5f;
		speedUpTimeInterval = 30;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastTime > speedUpTimeInterval) {
			lastTime = Time.time;
			SpeedUp ();
			level++;
			StartCoroutine ("UpdateLevel");
		}
		if (Input.GetKey(KeyCode.Escape)) {
			Time.timeScale = 0f;
			pausePanel.SetActive (true);
		}
	}

	public void UpdateScore() {
		score++;
		if (score > GlobalObjectController.Instance.highScore) {
			GlobalObjectController.Instance.highScore = score;
			highScoreText.text = score.ToString ();
		}
			
		scoreText.text = score.ToString ();
	}

	public void AddLive() {
		lives++;
		StartCoroutine ("UpdateLivesCounter");
	}

	public void SubLive() {
		lives--;
		StartCoroutine ("UpdateLivesCounter");
	}

	IEnumerator UpdateLevel() {
		float duration = 0.5f;
		int startSize = levelText.fontSize;
		for(float timer = 0; timer <= duration; timer += Time.deltaTime) {
			float progress = timer / duration;
			levelText.fontSize = (int)Mathf.Lerp(startSize, 2f * startSize, progress);
			yield return null;
		}
		levelText.text = level.ToString();
		for(float timer = 0; timer <= duration; timer += Time.deltaTime) {
			float progress = timer / duration;
			levelText.fontSize = (int)Mathf.Lerp( 2f * startSize, startSize, progress);
			yield return null;
		}
		levelText.fontSize = startSize;
	}

	IEnumerator UpdateLivesCounter() {
		float duration = 0.5f;
		Color32 endColor = new Color32(210, 30, 40, 255);
		for(float timer = 0; timer <= duration; timer += Time.deltaTime) {
			float progress = timer / duration;
			livesText.fontSize = (int)Mathf.Lerp(livesTextSize, 1.8f * livesTextSize, progress);
			livesText.color = Color32.Lerp(livesTextColor, endColor, progress);
			yield return null;
		}
		livesText.text = lives.ToString();
		for(float timer = 0; timer <= duration; timer += Time.deltaTime) {
			float progress = timer / duration;
			livesText.fontSize = (int)Mathf.Lerp( 1.8f * livesTextSize, livesTextSize, progress);
			livesText.color = Color32.Lerp(endColor, livesTextColor, progress);
			yield return null;
		}
		livesText.fontSize = livesTextSize;
		livesText.color = livesTextColor;
	}

	public void SpeedUp() {
		giftSpeed += 0.1f;
		fireballSpeed += 0.1f;
		FindObjectOfType<SnowmanController> ().normalSpeed += 0.1f;
		if(FindObjectOfType<GiftGeneratorController>().timeInterval > 0.5f)
			FindObjectOfType<GiftGeneratorController>().timeInterval -= 0.1f;
		if (FindObjectOfType<FireballGeneratorController> ().timeInterval > 0.5f)
			FindObjectOfType<FireballGeneratorController>().timeInterval -= 0.2f;
	}

	public void Resume() {
		Time.timeScale = 1f;
		clickSound.Play ();
		pausePanel.SetActive (false);
	}
	public void GameOver() {
		gameOverScreen.SetActive (true);
		this.enabled = false;
	}

	public void RestartGame() {
		clickSound.Play ();
		StartCoroutine ("RestartCoroutine");
	}

	IEnumerator RestartCoroutine() {
		yield return new WaitForSecondsRealtime (0.2f);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void Quit() {
		clickSound.Play ();
		StartCoroutine ("QuitCoroutine");
	}

	IEnumerator QuitCoroutine() {
		yield return new WaitForSecondsRealtime (0.2f);
		Application.Quit();
	}
}
