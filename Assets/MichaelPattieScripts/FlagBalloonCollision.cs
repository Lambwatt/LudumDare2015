using UnityEngine;
using System.Collections;

public class FlagBalloonCollision : MonoBehaviour {

	public BalloonData data;
	public BalloonDamager damager;
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other){
		//Debug.LogError("flagged baslloon");
		data.objectHittingBalloon = other.tag;
	}
}
