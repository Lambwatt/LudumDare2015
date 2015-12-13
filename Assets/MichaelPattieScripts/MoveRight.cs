using UnityEngine;
using System.Collections;

public class MoveRight : MonoBehaviour {

	public BalloonData data;
	
	// Update is called once per frame
	void Update () {
		if(data.dash)
			transform.position+=new Vector3(Time.deltaTime*(data.speed + data.speedBoost), 0, 0);
		else
			transform.position+=new Vector3(Time.deltaTime*data.speed, 0, 0);
	}
}
