using UnityEngine;
using System.Collections;

public class MoveLeft : MonoBehaviour {

	public float speed;
	
	// Update is called once per frame
	void Update () {
		transform.position+=new Vector3(Time.deltaTime*speed, 0, 0);
	}
}
