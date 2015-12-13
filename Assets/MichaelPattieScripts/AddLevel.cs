using UnityEngine;
using System.Collections;

public class AddLevel : MonoBehaviour {

	public string[] levels;
	public Transform player;
	public float sceneWidth;
	private int levelIndex = 0;
	private float nextLevelX = 0;
	private int levelCount = 0;

	// Use this for initialization
	void Start () {
		Application.LoadLevelAdditive(levels[levelIndex]);
		updateIndex();
	}
	
	// Update is called once per frame
	void Update () {
		if(player.position.x>=nextLevelX ){
			Application.LoadLevelAdditive(levels[levelIndex]);
			nextLevelX = player.position.x+sceneWidth;
			updateIndex();
		}
	}

	private void updateIndex(){
		levelIndex = (levelIndex+1)%levels.Length;
	}

	public float getNextScenePosition(){
		levelCount++;
		Debug.Log ("level count  = "+levelCount);
		return sceneWidth*(levelCount-1);
	}
}
