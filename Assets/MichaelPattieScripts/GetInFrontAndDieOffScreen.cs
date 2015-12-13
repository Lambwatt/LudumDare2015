using UnityEngine;
using System.Collections;

public class GetInFrontAndDieOffScreen : MonoBehaviour {


	private AddLevel manager;
	private Transform Player;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AddLevel>();
		Player = GameObject.FindGameObjectWithTag("Player").transform;
		transform.position = new Vector3(manager.getNextScenePosition(),0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if(Player.position.x - transform.position.x > manager.sceneWidth)
			Destroy(gameObject);
	}
}
