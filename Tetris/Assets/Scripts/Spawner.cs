using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] tetrisShapes;
	private GameObject currentObject;

    public void SpawnFirstShape() {
        int i = Random.Range (0, tetrisShapes.Length);
		if(i == 0) //shape = shapeO
			currentObject = Instantiate (tetrisShapes [i], transform.position + new Vector3(0.55f, 0.55f, 0), Quaternion.identity);
		else
			currentObject = Instantiate (tetrisShapes [i], transform.position, Quaternion.identity);
		currentObject.GetComponent<TetrisGameObjectController> ().enabled = true;
	}

	public void SpawnNextShape() {
		int next = FindObjectOfType<NextShape> ().next;
		if (next == 0)//shape = shapeO
			currentObject = Instantiate (tetrisShapes [next], transform.position + new Vector3 (0.55f, 0.55f, 0), Quaternion.identity);
		else
			currentObject = Instantiate (tetrisShapes [next], transform.position, Quaternion.identity);
		currentObject.GetComponent<TetrisGameObjectController> ().enabled = true;
	}
}
