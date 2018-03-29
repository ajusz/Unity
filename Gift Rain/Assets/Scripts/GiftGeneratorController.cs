using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftGeneratorController : MonoBehaviour {

	float lastTime = 0;
	public float timeInterval = 0.9f;
	float giftPositionY = 9.7f;
	float bound = 3.8f;
	public GameObject[] giftsPrefabs;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastTime > timeInterval) {
			lastTime = Time.time;
			GenerateGift();
		}
	}

	void GenerateGift(){
		int i = Random.Range (0, giftsPrefabs.Length);
		float giftPositionX = Random.Range (-bound, bound);
		GameObject newObject = Instantiate(giftsPrefabs[i], new Vector3(giftPositionX, giftPositionY, transform.position.z), Quaternion.identity);
		newObject.GetComponent<GiftController> ().enabled = true;
	}
}
