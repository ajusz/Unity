using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballController : MonoBehaviour {

	private Rigidbody rb;
	public ParticleSystem particlePrefab;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (0, 200, 0);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Snowball")
			return;
		ParticleSystem particle = Instantiate (particlePrefab, transform.position, Quaternion.identity);
		particle.GetComponent<AudioSource> ().Play ();
		transform.gameObject.SetActive(false);
		Destroy(transform.gameObject, particle.main.duration);
		Destroy(particle , particle.main.duration);
	}
		
}