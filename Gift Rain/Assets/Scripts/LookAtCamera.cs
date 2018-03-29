using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
	public GameObject player;
	float speed = 1.1f;
	float lookAtPosition = 0;
	// Use this for initialization
	void LateUpdate() {
		lookAtPosition = speed * player.transform.position.x;
		transform.eulerAngles = new Vector3 (0, lookAtPosition, 0);
	}
}
