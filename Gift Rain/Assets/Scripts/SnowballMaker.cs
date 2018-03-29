using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballMaker : MonoBehaviour {

	float lastTime = 0;
	float timeInterval = 0.3f;
	float bonusTimeInterval = 0.02f;
	public GameObject snowballPrefab;
	public int bonusSnowballs = 0;

	// Update is called once per frame
	void Update () {
		if (Time.time - lastTime > timeInterval) {
			lastTime = Time.time;
			if(bonusSnowballs > 0) {
				StartCoroutine ("Make3Snowballs");
			}
			else
				MakeSnowball();
		}
	}

	GameObject MakeSnowball(){
		GameObject currentObject = Instantiate (snowballPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
		currentObject.GetComponent<SnowballController> ().enabled = true;
		Destroy (currentObject, 5);
		return currentObject;
	}

	IEnumerator Make3Snowballs() {
		float[] forces = {-40, 0, 40};
		for (int i = 0; i < 3; i++) {
				MakeSnowball ().GetComponent<Rigidbody> ().AddForce (forces[i], 0, 0);
			yield return new WaitForSeconds(bonusTimeInterval);
		}
	}
}
