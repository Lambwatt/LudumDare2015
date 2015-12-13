using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public SpawnEnemy[] spawners;
	private Transform player;

	// Use this for initialization
	void Start () {
		if(spawners != null){
			foreach(SpawnEnemy s in spawners){
				s.spawnAnEnemy();
			}
		}

		player = GameObject.FindGameObjectWithTag("Player").transform;
		Debug.Log ("player = "+player);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(player.position.x-transform.position.x);
		if(player.position.x-transform.position.x > 48.0f)
			Destroy (this.gameObject);
	}
}
