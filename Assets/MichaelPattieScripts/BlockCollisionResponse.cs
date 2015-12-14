using UnityEngine;
using System.Collections;

public class BlockCollisionResponse : MonoBehaviour{

	public int damage;

	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("PlayerPart")){

			col.gameObject.transform.GetComponentInParent<BalloonDamager>().damagePlayer(damage);


			//Spawn particle system
			//Destroy(this);
		}
	}

}
