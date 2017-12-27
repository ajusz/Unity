using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGameObjectController : MonoBehaviour {

    float scale = 1.1f;
	float lastTime = 0f;
	float timeBetweenSideSteps = 0.2f;
	float timeBetweenDownSteps = 0.05f;
	float[] lastStep = new float[3]; //left, right, down
	public AudioSource rotationAudio;
	public AudioSource sideMoveAudio;
	public AudioSource stopAudio;

	// Use this for initialization
	void Start () {
		InsertIntoGrid ();
		MoveDown();
	}
	
	// Update is called once per frame
	void Update () {
		DeleteFromGrid ();
		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (Time.time - lastStep [0] > timeBetweenSideSteps) {
				lastStep [0] = Time.time;
				SideMove (new Vector3 (-1, 0, 0));
			}
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			if (Time.time - lastStep [1] > timeBetweenSideSteps) {
				lastStep [1] = Time.time;
				SideMove (new Vector3 (1, 0, 0));
			}
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			transform.Rotate (0, 0, -90);
			if (CanChangePosition ())
				rotationAudio.Play ();
			else
				transform.Rotate (0, 0, 90);
			
		}
		InsertIntoGrid ();

		if (Input.GetKey (KeyCode.DownArrow) || Time.time - lastTime >= GameController.timerConst) {
			if (Time.time - lastStep [2] > timeBetweenDownSteps) {
				lastStep [2] = Time.time;
				lastTime = Time.time;
				MoveDown ();
			}
		}
	}

	void SideMove(Vector3 v){
		transform.position += v * scale;
		if (CanChangePosition ())
			sideMoveAudio.Play ();
		else
			transform.position -= v * scale;
	}

    bool CanChangePosition() {
		foreach (Transform child in transform) {
			Vector2 v = Grid.Rescale (child.position);
			if (!Grid.IsOnBoard (v))
				return false;
			if (Grid.grid [(int)Math.Floor(v.y), (int)Math.Floor(v.x)] != null)
				return false;
		}
		return true;			
	}

	void DeleteFromGrid() {
		foreach (Transform child in transform) {
			Vector2 v = Grid.Rescale (child.position);
			Grid.grid [(int)Math.Floor(v.y), (int)Math.Floor(v.x)] = null;
		}
	}

	void InsertIntoGrid() {
		foreach (Transform child in transform) {
			Vector2 v = Grid.Rescale (child.position);
			Grid.grid [(int)Math.Floor(v.y), (int)Math.Floor(v.x)] = child;
		}
	}

	void MoveDown() {
		DeleteFromGrid ();
		transform.position += new Vector3 (0, -1, 0) * scale;
		if (CanChangePosition ()) {
			transform.position += new Vector3 (0, -1, 0) * scale;
			if (!CanChangePosition ())
				stopAudio.Play ();
			transform.position -= new Vector3 (0, -1, 0) * scale;
			InsertIntoGrid ();
		}
		else {
			transform.position -= new Vector3 (0, -1, 0) * scale;
			InsertIntoGrid ();
			int numOfFullRows = FindObjectOfType<Grid>().DeleteFullRows (scale);
			GameController gc = FindObjectOfType<GameController> ();
			gc.UpdateResults (numOfFullRows);
			if (TooHigh ())
				gc.EndGame ();
			else {
				FindObjectOfType<Spawner> ().SpawnNextShape ();
				FindObjectOfType<NextShape> ().NextRandomShape ();
			}
			enabled = false;
		}
	}

	bool TooHigh() {
		foreach (Transform child in transform) {
			Vector2 v = Grid.Rescale (child.position);
			if ((int)Math.Floor(v.y) > 22)
				return true;
		}
		return false;
	}
}
