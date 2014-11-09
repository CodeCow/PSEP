using System.Collections;
using UnityEngine;
using Pose = Thalmic.Myo.Pose;
using VibrationType = Thalmic.Myo.VibrationType;

class PlayerController : MonoBehaviour {
	public float moveSpeed = 3f;
	public float gridSize = 1f;
	private enum Orientation {
		Horizontal,
		Vertical
	};
	private enum PlayerDirection {
		Up=1,
		Right=2,
		Down=3,
		Left=4,

		Nil = -1
	};
	private Orientation gridOrientation = Orientation.Horizontal;

	private bool allowDiagonals = false;
	private bool correctDiagonalSpeed = true;
	public Vector2 input;
	public Vector3 dir;
	private bool isMoving = false;
	public Vector3 startPosition;
	public Vector3 endPosition;
	private float t;
	private float factor;
	public GameObject myo = null;
	private Vector3 camPanning;
	private Pose _lastPose = Pose.Unknown;
	public Vector3 newPosition;
	private PlayerDirection pd= PlayerDirection.Up;
	private GameObject BulletPrefab;
	private int pdInt = 1;
	public void Start(){
		dir.x = -1;
		BulletPrefab = Resources.Load("PreFabs/Sphere",typeof(GameObject)) as GameObject;
	}


	public void Update() {
	
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

		if (thalmicMyo.pose != _lastPose) {
			_lastPose = thalmicMyo.pose;
		
			CameraGestures(ref thalmicMyo);
			if (!isMoving) {
				if (thalmicMyo.pose == Pose.WaveOut) 
					//input.x = -1;

				if (Mathf.Abs (input.x) > Mathf.Abs (input.y)) {
					input.y = 0;
				} else {
					input.x = 0;
				}
			
				if (input.sqrMagnitude > 0) {
					Vector2 abs = new Vector2 (Mathf.Abs (input.x), Mathf.Abs ((input.y)));
					if (abs.x > abs.y) {
						input.y = 0;
					} else {
						input.x = 0;
					}
				}
			
			}
		}
			
		StartCoroutine(move (transform));

		
	}
	
	public IEnumerator move(Transform transform) {
		isMoving = true;
		startPosition = transform.position;
		t = 0;

		if (input != Vector2.zero) {
			if (input.sqrMagnitude > 0) {
				dir.x = Mathf.RoundToInt (input.x); 
				dir.y = Mathf.RoundToInt (input.y);     
				dir.Normalize ();
			}
		}

		if (gridOrientation == Orientation.Horizontal) {
			endPosition = new Vector3 (startPosition.x + Mathf.RoundToInt (dir.x) * gridSize,
			                          startPosition.y, startPosition.z + Mathf.RoundToInt (dir.y) * gridSize);
		} 
		
		if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) {
			factor = 0.7071f;
		} else {
			factor = 3.4f;
		}
		
		while (t < 1f) {
			t += Time.deltaTime * (moveSpeed / gridSize) * factor;
			transform.position = Vector3.Lerp (startPosition, endPosition + new Vector3 (dir.x, 0, dir.y), t);
			yield return null;
		}
		
		isMoving = false;
		yield return 0;
	
	}

//	IEnumerator CameraPan (Transform target){
//		while (Vector3.Distance(transform.position, target.position) > 0.05f) {
//			camPanning = Vector3.Lerp (transform.position, camPanning, Time.deltaTime * 20f);
//			Debug.Log("Called");
//			yield return new WaitForSeconds(5);
//		}

public void CameraGestures(ref ThalmicMyo thalmicMyo)
{
		//Vibrate the Myo armband when a fist is made.
//		if (thalmicMyo.pose == Pose.WaveIn && thalmicMyo.pose != _lastPose) {
//			thalmicMyo.Vibrate (VibrationType.Medium);
		if (thalmicMyo.pose == Pose.WaveOut) {	
			//pd = getDirection (dir);     
			Debug.Log (pdInt.ToString ());
			if (pdInt < 4)
				pdInt += 1;
			else
				pdInt = 1;

			if (pdInt == 1)
				dir = new Vector2 (0, 1);
			if (pdInt == 2)
				dir = new Vector2 (1, 0);
			if (pdInt == 3)
				dir = new Vector2 (0, -1);
			if (pdInt == 4)
				dir = new Vector2 (-1, 0);

			camPanning = new Vector3 (0, camPanning.y + -90, 0);

//			StartCoroutine(CameraPan(transform));
//			thalmicMyo.Vibrate (VibrationType.Short);
		}if (thalmicMyo.pose == Pose.WaveIn) {
			Debug.Log(pdInt.ToString());
			//pd = getDirection(dir);
			if(pdInt <= 0)
				pdInt = 4;
			else
				pdInt -= 1;

			if(pdInt == 1)
				dir = new Vector2(0,1);
			if(pdInt == 2)
				dir = new Vector2(1,0);
			if(pdInt == 3)
				dir = new Vector2(0,-1);
			if(pdInt == 4)
				dir = new Vector2(-1,0);

			camPanning = new Vector3 (0, camPanning.y + 90, 0);

			transform.eulerAngles = new Vector3(transform.eulerAngles.x+90,transform.eulerAngles.y,transform.eulerAngles.z); 
//			StartCoroutine(CameraPan(transform));



		}
		Camera.main.transform.eulerAngles = camPanning;


		if (thalmicMyo.pose == Pose.Fist) 
		{
			Instantiate(BulletPrefab);
		}

	}
	private PlayerDirection getDirection(Vector2 dir)
	{

		if (pd != (PlayerDirection)1)
		if(dir == new Vector2(-1,0))
			return PlayerDirection.Left;
		if(dir == new Vector2(1,0))
			return PlayerDirection.Right;
		if(dir == new Vector2(0,1))
			return PlayerDirection.Up;
		if(dir == new Vector2(0,-1))
			return PlayerDirection.Down;
		
		return PlayerDirection.Nil;
	}
}
