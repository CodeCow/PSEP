//using UnityEngine;
//using System.Collections;
//using Pose = Thalmic.Myo.Pose;
//using VibrationType = Thalmic.Myo.VibrationType;
//
//public class CameraTest : MonoBehaviour {
//	public GameObject myo = null;
//	private Vector3 direction;
//	private Pose _lastPose = Pose.Unknown;
//	private GameObject NewBullet;
//	// Use this for initialization
//	void Start () {
//		NewBullet = Resources.Load ("PreFabs/Capsule", typeof(GameObject)) as GameObject;
//
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
//
//		if (thalmicMyo.pose != _lastPose) {
//			_lastPose = thalmicMyo.pose;
//
//			// Vibrate the Myo armband when a fist is made.
//			//if (thalmicMyo.pose == Pose.WaveIn && thalmicMyo.pose != _lastPose) {
//			//	thalmicMyo.Vibrate (VibrationType.Medium);
//			//} else if (thalmicMyo.pose == Pose.WaveOut) {	
//			//	direction = new Vector3 (0, direction.y + -90, 0);
//				//thalmicMyo.Vibrate (VibrationType.Short);
//			//} else if (thalmicMyo.pose == Pose.WaveIn) {
//				//direction = new Vector3 (0, direction.y + 90, 0);
//			//}
//
//		  if (thalmicMyo.pose == Pose.Fist) {
//				Rigidbody BulletInstance;
//				BulletInstance = Instantiate(NewBullet);
//				BulletInstance.AddForce(transform.forward*200);
//			}
////			camera.transform.eulerAngles = direction;
////			transform.position = Vector3.Lerp(transform.position, direction, Time.deltaTime);
//		}
//		
//	}
//}
