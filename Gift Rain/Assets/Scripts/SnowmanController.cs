using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanController : MonoBehaviour {

	float bound;
	public float normalSpeed = 3.2f;
	public float superSpeed;
	public float speed;
	GameObject santaHat;
	public GameObject puddlePrefab;
	public AudioSource bonusSound;
	public AudioSource starSound;
	bool dead;
	bool triggered;
	float bonusDuration;

	// Use this for initialization
	void Start () {
		bound = 3.5f;
		speed = normalSpeed;
		superSpeed = 6.5f;
		dead = false;
		triggered = false;
		bonusDuration = 15;
	}
	
	// Update is called once per frame
	void Update () {
		triggered = false;
		if (!dead) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			if (Input.GetKey (KeyCode.LeftArrow)) {
				if (transform.position.x > -bound) {
					GetComponent<Rigidbody> ().velocity = new Vector3 (-1, 0, 0) * speed;
					transform.rotation = Quaternion.Euler (0, -90, 0);
				}
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				if (transform.position.x < bound) {
					GetComponent<Rigidbody> ().velocity = new Vector3 (1, 0, 0) * speed;
					transform.rotation = Quaternion.Euler (0, 90, 0);
				}
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (triggered)
			return;
		triggered = true;
		GameController gc = FindObjectOfType<GameController> ();
		if (collider.tag == "Star") {
			starSound.Play ();
			gc.UpdateScore ();
			Destroy (collider.gameObject);
		} else if (collider.tag == "Heart") {
			bonusSound.Play ();
			gc.AddLive ();
			Destroy (collider.gameObject);
		} else if (collider.tag == "BonusSpeed") {
			bonusSound.Play ();
			speed = superSpeed;
			StartCoroutine ("BonusSpeedTimer");
			Destroy (collider.gameObject);
		} else if (collider.tag == "BonusSnowballs") {
			bonusSound.Play ();
			FindObjectOfType<SnowballMaker> ().bonusSnowballs++;
			StartCoroutine ("BonusSnowballsTimer");
			Destroy (collider.gameObject);
		} else if (collider.tag == "Fire") {
			collider.GetComponent<CapsuleCollider> ().enabled = false;
			gc.SubLive ();
			if (gc.lives > 0) {
				StartCoroutine ("Blink");
			} else {
				collider.GetComponent<Rigidbody> ().velocity = new Vector3 (0, -1, 0);
				Die ();
			}
		}
	}

	IEnumerator BonusSnowballsTimer() {
		yield return new WaitForSeconds(bonusDuration);
		FindObjectOfType<SnowballMaker> ().bonusSnowballs--;
	}

	IEnumerator BonusSpeedTimer() {
		yield return new WaitForSeconds(bonusDuration);
		speed = normalSpeed;
	}

	IEnumerator Blink() {
		for (int i = 0; i < 6; i++) {
			foreach (Transform child in transform) {
				if (child.name == "SnowmanBody") {
					foreach (Transform child1 in child) {
						child1.GetComponent<Renderer> ().enabled = !child1.GetComponent<Renderer> ().enabled;
					}
				} else {
					child.GetComponent<Renderer> ().enabled = !child.GetComponent<Renderer> ().enabled;
				}
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	void Die() {
		dead = true;
		Destroy(transform.GetComponent<Rigidbody>());
		FindObjectOfType<SnowballMaker> ().enabled = false;
		FindObjectOfType<GiftGeneratorController> ().enabled = false;
		FindObjectOfType<FireballGeneratorController> ().enabled = false;
		StartCoroutine ("MeltSnowman");
	}

	IEnumerator MeltSnowman() {
		float duration = 1f;
		Transform snowmanBody = transform.Find ("SnowmanBody");
		Vector3 start = snowmanBody.localScale;
		Vector3 zeros = new Vector3 (0, 0, 0);
		GameObject puddle = Instantiate (puddlePrefab, transform.position, Quaternion.identity);
		Vector3 puddleEndScale = puddle.transform.localScale;
		puddle.transform.localScale = zeros;
		for (float timer = 0; timer <= duration; timer += Time.deltaTime) {
			float progress = timer / duration;
			snowmanBody.localScale = Vector3.Lerp (start, zeros, progress);
			puddle.transform.localScale = Vector3.Lerp (zeros, puddleEndScale, progress);
			yield return null;
		}
		puddle.transform.localScale = puddleEndScale;
		foreach (Transform child in transform) {
			child.gameObject.AddComponent<Rigidbody> ();
			child.gameObject.GetComponent<Rigidbody> ().useGravity = true;
			if (child.name == "Santa-Hat") {
				child.GetComponent<BoxCollider> ().enabled = true;
			}
		}
		GameController gc = FindObjectOfType<GameController> ();
		gc.GameOver();
	}
}
