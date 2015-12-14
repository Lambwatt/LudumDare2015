using UnityEngine;
using System.Collections;

public class EnemyStandardization : MonoBehaviour {
	public GameObject[] enemies;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		enemies = GameObject.FindGameObjectsWithTag ("enemies");
		foreach (GameObject enemy in enemies) 
		{
			float PosX = enemy.transform.position.x;
			float PosY = enemy.transform.position.y;
			enemy.transform.position = new Vector3(PosX, PosY, 0);
		}
	}
}
