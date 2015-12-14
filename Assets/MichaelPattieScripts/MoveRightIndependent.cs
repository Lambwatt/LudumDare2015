using UnityEngine;
using System.Collections;

public class MoveRightIndependent : MonoBehaviour {

	public float speed;
	// Update is called once per frame
	void Start ()
	{
		transform.Rotate (0, 90f, 90f);
	}
	void Update () {
		transform.position+=new Vector3(Time.deltaTime*speed, 0, 0);
	}
}
