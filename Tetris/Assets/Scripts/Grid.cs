using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour{

	public static int columns = 11;
	public static int rows = 30;
	public static Transform[,] grid = new Transform[rows, columns];
	public GameObject particlePrefab;
	public AudioSource deleteRowSound; 

	public static Vector2 Rescale(Vector3 position) {
		return new Vector2 ((position.x + 6f) / 1.1f, (position.y) / 1.1f);
	}
		
	public static bool IsOnBoard(Vector2 gridPosition) {
		return ((int)Math.Floor(gridPosition.x) >= 0 && (int)Math.Floor(gridPosition.x) < columns && (int)Math.Floor(gridPosition.y) >= 0); 
	}

	public void DeleteRow(int y) {
		for (int x = 0; x < columns; x++) {
			Destroy(Instantiate (particlePrefab, grid[y, x].position, Quaternion.identity), 2f);
			StartCoroutine ("ReduceBlock", grid [y, x]);
			grid [y, x] = null;
			deleteRowSound.Play ();
		}
	}

	IEnumerator ReduceBlock(Transform block) {
		float duration = 0.4f;
		Vector3 start = block.localScale;
		Vector3 zeros = new Vector3 (0, 0, 0);
		for (float timer = 0; timer <= duration; timer += Time.deltaTime) {
			float progress = timer / duration;
				block.localScale = Vector3.Lerp (start, zeros, progress);
			yield return null;
		}
		Destroy (block.gameObject);
	}

	public void MoveDownRow(int y, float scale) {
		for (int x = 0; x < columns; x++) {
			grid [y - 1, x] = grid [y, x];
			grid[y, x] = null;
			if(grid[y - 1, x] != null)
				grid [y - 1, x].position += new Vector3 (0, -1, 0) * scale;
		}
	}

	public void MoveDownAllAbove(int y, float scale) {
		for (int i = y + 1; i < rows; i++) {
			MoveDownRow (i, scale);
		}
	}

	public bool FullRow(int y) {
		for (int x = 0; x < columns; x++) {
			if (grid [y, x] == null)
				return false;
		}
		return true;
	}

	public int DeleteFullRows(float scale) {
		int counter = 0;
		for (int y = 0; y < rows; y++) {
			if(FullRow(y)) {
				counter++;
				DeleteRow(y);
				MoveDownAllAbove(y, scale);
				y--;
			}
		}
		return counter;
	}
}
