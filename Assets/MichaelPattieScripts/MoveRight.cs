using UnityEngine;
using System.Collections;

public class MoveRight : MonoBehaviour {

	public BalloonData data;
	
	// Update is called once per frame
	void Update () {

		float powerUpSpeed = 0.0f;

		if(data.speedUpPowerOn)
			powerUpSpeed = data.speedUpPower;
		else if(data.slowDownPowerOn)
			powerUpSpeed = data.slowDownPower;


		if(data.dash)
			transform.position+=new Vector3(Time.deltaTime*(data.speed + data.speedBoost + powerUpSpeed), 0, 0);
		else
			transform.position+=new Vector3(Time.deltaTime*(data.speed+powerUpSpeed), 0, 0);
	}
}
