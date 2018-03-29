using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballGeneratorController : MonoBehaviour {

	float lastTime = 0;
	public float timeInterval = 2f;
	float fireballPositionY = 10f;
	public GameObject fireballPrefab;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Time.time - lastTime > timeInterval) {
			lastTime = Time.time;
			GenerateFireball();
		}
	}

	void GenerateFireball(){
		//float fireballPositionX = Random.Range (-bound, bound);
		GameObject snowman = GameObject.Find("Snowman");
		if (snowman) {
			float fireballPositionX = snowman.transform.position.x;
			GameObject newObject = Instantiate (fireballPrefab, new Vector3 (fireballPositionX, fireballPositionY, transform.position.z), Quaternion.identity);
			newObject.GetComponent<FireballController> ().enabled = true;
			Destroy (newObject, 15);
		}
	}
}
