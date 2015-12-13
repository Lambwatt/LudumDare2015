using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	public GameObject[] spawnables;
	// Use this for initialization

	public void spawnAnEnemy(){
		if(spawnables.Length > 0){
			int index = Mathf.FloorToInt(Random.value*(float)spawnables.Length);
			Instantiate(spawnables[index], transform.position, Quaternion.identity);
			Destroy(this);
		}
	}
}
