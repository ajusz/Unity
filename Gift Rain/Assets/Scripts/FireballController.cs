using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		float speed = FindObjectOfType<GameController> ().fireballSpeed;
		rb = GetComponent<Rigidbody> ();
		rb.velocity =  new Vector3 (0, -1, 0) * speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
