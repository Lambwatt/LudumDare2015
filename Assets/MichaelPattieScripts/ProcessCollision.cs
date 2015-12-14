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


	[HideInInspector]public bool speedPowerOn;
	[HideInInspector]public bool inflatCoefficientPowerOn;
	[HideInInspector]public bool deflateCoefficientPowerOn;
	[HideInInspector]public bool speedUpPowerOn;
	[HideInInspector]public bool slowDownPowerOn;
	[HideInInspector]public bool immunityOn;
	void applyCollisionResults(string colTag){
		switch(colTag){
//		case "Sandbag":
//			data.cancelPowerups();
//			data.deflateCoefficientPowerOn = true;
//			break;
//		case "Sandbag":
//			data.cancelPowerups();
//			data.deflatePower = true;
//			break;
//		case "Sandbag":
//			data.cancelPowerups();
//			data.deflatePower = true;
//			break;
//		case "Sandbag":
//			data.cancelPowerups();
//			data.deflatePower = true;
//			break;
		default:
			break;
		}
	}
}
