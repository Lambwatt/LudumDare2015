using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public Transform target;
	public float  distance;
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position + new Vector3(0, 0, distance);
	}
}


//#pragma strict
//
//var target:Transform;
//var distance:float;
//var lift:float;
//
//function Update () {
//	//Debug.Log("Hello?");
//	//Debug.Log("addition = "+ Vector3(0, lift, distance));
//	transform.position = target.position + Vector3(0, lift, distance);
//	
//	//Debug.Log("pos = " +transform.position);
//	//transform.LookAt(target);
//	//transform.Rotate(Vector3(0,0,1), 180);
//}