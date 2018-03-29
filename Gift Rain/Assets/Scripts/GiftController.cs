using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftController : MonoBehaviour {

	float speed;
	private Rigidbody rb;
	public GameObject starPrefab;
	public GameObject[] bonusesPrefabs;
	// Use this for initialization
	void Start () {
		speed = FindObjectOfType<GameController> ().giftSpeed;
		rb = GetComponent<Rigidbody> ();
		rb.velocity = new Vector3 (0, -1, 0) * speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Snowball") {
			int i = Random.Range (0, 9);
			if(i == 0) { // bonus
				int j = Random.Range (-bonusesPrefabs.Length + 1, bonusesPrefabs.Length);
				j = Mathf.Abs (j);
				GameObject bonus = Instantiate(bonusesPrefabs[j], transform.position, Quaternion.identity);
				bonus.GetComponent<Rigidbody>().velocity = new Vector3 (0, -1, 0) * speed * 1.8f;
				Destroy (bonus, 10);
			}
			else {
				GameObject star = Instantiate(starPrefab, transform.position, Quaternion.identity);
				star.GetComponent<Rigidbody>().velocity = new Vector3 (0, -1, 0) * speed * 1.5f;
				Destroy (star, 10);
			}
			transform.gameObject.SetActive(false);
		} 
		Destroy(transform.gameObject);
	}
}
