using System.Collections;
using UnityEngine;

// contribution Author: Eric Haines (Eric5h5) on Unify Community
class PlayerController: MonoBehaviour {
	public float moveSpeed = 3f;
	public float gridSize = 1f;
	public Vector3 direction;

	private enum Orientation {
		Horizontal,
		Vertical
	};
	private Orientation gridOrientation = Orientation.Horizontal;
	private bool allowDiagonals = false;
	private bool correctDiagonalSpeed = true;
	private Vector2 input;
	private bool isMoving = true;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float t;
	private float factor;
	
	public void Update() {
		transform.position += transform.forward * moveSpeed * Time.deltaTime;

		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			direction = new Vector3(0,90,0);
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			direction = new Vector3(0,-90,0);
		}
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			direction = new Vector3(0,0,0);
		}
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			direction = new Vector3(0,180,0);
		}
			transform.eulerAngles = direction;
	}
	
	public IEnumerator move(Transform transform) {
		isMoving = true;
		startPosition = transform.position;
		t = 0;
		
		if(gridOrientation == Orientation.Horizontal) {
			endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
			                          startPosition.y, startPosition.z + System.Math.Sign(input.y) * gridSize);
		} else {
			endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
			                          startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
		}
		
		if(allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) {
			factor = 0.7071f;
		} else {
			factor = 1.5f;
		}
		
		while (t < 1f) {
			t += Time.deltaTime * (moveSpeed/gridSize) * factor;
			transform.position = Vector3.Lerp(startPosition, endPosition, t);
			yield return null;
		}
		
		isMoving = false;
		yield return 0;
	}
}