using UnityEngine;
using System.Collections;

public class BulletResponse : MonoBehaviour{

	public int damage;

	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("PlayerProjectils")){

			//col.gameObject.transform.GetComponentInParent<BalloonDamager>().damagePlayer(damage);
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<Score>().updateScore(damage*25);

			//Spawn particle system
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
	}

}
