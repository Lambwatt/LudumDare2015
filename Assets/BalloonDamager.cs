using UnityEngine;
using System.Collections;

public class BalloonDamager : MonoBehaviour {

	public BalloonData data;

	void damagePlayer(int damage){
		if(!data.immunityOn)
			data.health -= damage;
	}
}
