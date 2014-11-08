using System.Collections;
using UnityEngine;

class PlayerController : MonoBehaviour {
	public float moveSpeed = 3f;
	public float gridSize = 1f;
	private enum Orientation {
		Horizontal,
		Vertical
	};
	private Orientation gridOrientation = Orientation.Horizontal;
	private bool allowDiagonals = false;
	private bool correctDiagonalSpeed = true;
	public Vector2 input;
	public Vector2 dir;
	private bool isMoving = false;
	public Vector3 startPosition;
	public Vector3 endPosition;
	private float t;
	private float factor;
	public void Update() {
	
		if (!isMoving) {
			input = new Vector2(Mathf.CeilToInt(Input.GetAxis("Horizontal")), Mathf.CeilToInt(Input.GetAxis("Vertical")));
		/*	if (!allowDiagonals) {
				if (Mathf.Abs(input.x) > Mathf.Abs(input.y)) {
					input.y = 0;
				} else {
					input.x = 0;
				}
			}
*/	      if(input.sqrMagnitude > 0)
			{
				Vector2 abs = new Vector2(Mathf.Abs(input.x), Mathf.Abs((input.y)));
				if(abs.x > abs.y)
				{
					input.y = 0;
				}
				else
				{
					input.x = 0;
				}
			}
			
				StartCoroutine(move(transform));

		}
	}
	
	public IEnumerator move(Transform transform) {
		isMoving = true;
		startPosition = transform.position;
		t = 0;

		if(input != Vector2.zero)
		{
			if(input.sqrMagnitude > 0)
			{
				dir.x = Mathf.RoundToInt(input.x); 
				dir.y = Mathf.RoundToInt(input.y);     
				dir.Normalize();
			}
		}

		if(gridOrientation == Orientation.Horizontal) {
			endPosition = new Vector3(startPosition.x + Mathf.RoundToInt(dir.x) * gridSize,
			                          startPosition.y, startPosition.z + Mathf.RoundToInt(dir.y) * gridSize);
		} 
		
		if(allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) {
			factor = 0.7071f;
		} else {
			factor = 3.4f;
		}
		
		while (t < 1f) {
			t += Time.deltaTime * (moveSpeed/gridSize) * factor;
			transform.position = Vector3.Lerp(startPosition, endPosition + new Vector3(dir.x,0,dir.y),t);
			yield return null;
		}
		
		isMoving = false;
		yield return 0;
	}
}