using UnityEngine;
using System.Collections;

public class BalloonDamager : MonoBehaviour {

	public BalloonData data;

	public void damagePlayer(int damage){
//		Debug.Log ("Damage = "+damage);
		if(!data.immunityOn){
			data.health -= damage;
//			Debug.Log ("health is now "+data.health);
		}
	}
}
