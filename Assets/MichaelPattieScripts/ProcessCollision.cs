using UnityEngine;
using System.Collections;

public class ProcessCollision : MonoBehaviour {

	public BalloonData data;

	// Update is called once per frame
	void Update () {

		if(data.objectHittingBalloon != null){
			//Debug.LogError ("detected balloon collision");

			//handlecollision

			data.objectHittingBalloon = null;
		}

		if(data.objectHittingBasket != null){
			//Debug.LogError ("detected basket collision");
			
			//handlecollision
			
			data.objectHittingBasket = null;
		}
	}
}
