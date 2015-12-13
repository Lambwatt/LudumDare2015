using UnityEngine;
using System.Collections;

public class ProcessCollision : MonoBehaviour {

	public BalloonData data;

	// Update is called once per frame
	void Update () {

		if(data.objectHittingBalloon != null){
			applyCollisionResults(data.objectHittingBalloon);

			data.objectHittingBalloon = null;
		}

		if(data.objectHittingBasket != null){
			applyCollisionResults(data.objectHittingBasket);
			
			data.objectHittingBasket = null;
		}
	}

	void applyCollisionResults(string colTag){
//		switch(colTag){
//		case "Sandbag":
//			data.cancelPowerups();
//			data.deflatePower = true;
//			break;
//		default:
//			break;
//		}
	}
}
