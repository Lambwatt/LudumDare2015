using UnityEngine;
using System.Collections;

public class CheckDifficulty : MonoBehaviour {

	public int difficulty;

	// Use this for initialization
	void Start () {
		if(difficulty>GameObject.FindGameObjectWithTag("GameManager").GetComponent<AddLevel>().levelCount)
			Destroy (gameObject);
	}
}
