using UnityEngine;
using System.Collections;

public class AddLevel : MonoBehaviour {

	public GameObject[] levels;
	public Transform player;
	public float screenWidth;
	private int levelIndex = 0;
	private float lastLevelX = 0;

	// Use this for initialization
	void Start () {
		Instantiate(levels[levelIndex], new Vector3(player.position.x-(screenWidth*.25f),0,0), Quaternion.identity);
		updateIndex();
		Instantiate(levels[levelIndex], new Vector3(player.position.x+(screenWidth*.75f),0,0), Quaternion.identity);
		updateIndex();
	}
	
	// Update is called once per frame
	void Update () {
		if(player.position.x-lastLevelX > screenWidth){
			Instantiate(levels[levelIndex], new Vector3(player.position.x+(screenWidth*.75f),0,0), Quaternion.identity);
			lastLevelX = player.position.x;
			updateIndex();
		}
	}

	private void updateIndex(){
		levelIndex = (levelIndex+1)%levels.Length;
	}
}
