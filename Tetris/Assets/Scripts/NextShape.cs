using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextShape : MonoBehaviour {

    public GameObject[] tetrisShapes;
    public int next;
	public GameObject nextShape;

    public void NextRandomShape()
    {
        next = Random.Range(0, tetrisShapes.Length);
		Destroy (nextShape);
		tetrisShapes [next].transform.localScale = new Vector3 (4, 4, 2);
		if (next == 0)
			nextShape = Instantiate(tetrisShapes[next], transform.position+ new Vector3(0, -3.3f, -1.1f), Quaternion.identity);
		else if(next == 3)
			nextShape = Instantiate(tetrisShapes[next], transform.position+ new Vector3(2.2f, -3.3f, -1.1f), Quaternion.identity);
		else
			nextShape = Instantiate(tetrisShapes[next], transform.position+ new Vector3(0, -5.5f, -1.1f), Quaternion.identity);
		tetrisShapes [next].transform.localScale = new Vector3 (1, 1, 1);
    }
}