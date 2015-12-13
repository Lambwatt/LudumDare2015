using UnityEngine;
using System.Collections;

public class BlockCollisionResponse : MonoBehaviour{

	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Player")){
			//Spawn particle system
			Destroy(this);
		}
	}

}
