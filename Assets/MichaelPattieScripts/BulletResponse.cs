using UnityEngine;
using System.Collections;

public class BulletResponse : MonoBehaviour{

	public int points;

	void OnTriggerEnter(Collider col){
		Debug.Log ("When hit, found "+col.gameObject.tag);
		if(col.gameObject.CompareTag("PlayerProjectile")){

			//col.gameObject.transform.GetComponentInParent<BalloonDamager>().damagePlayer(damage);
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<Score>().updateScore(points);

			//Spawn particle system
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
	}

}
