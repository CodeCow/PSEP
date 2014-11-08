using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Vector3 direction;
	public float speed = 12f;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += transform.forward * speed * Time.deltaTime;


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
}
