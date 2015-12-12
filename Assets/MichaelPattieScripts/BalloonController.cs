using UnityEngine;
using System.Collections;

public class BalloonController : MonoBehaviour {

	public BalloonData data;
	public Balloon balloon;
	
	// Update is called once per frame
	void Update () {
		int inputHash = 0;
		if(Input.GetKey(KeyCode.A)){
			inputHash+=1;
		}

		if(Input.GetKey(KeyCode.B)){
			inputHash+=2;
		}
		data.dash = false;
		switch(inputHash){
		case 1:
			balloon.inflate();
			break;
		case 2:
			//shoot
			break;
		case 3:
			data.dash = true;
			break;

		case 0:
		default:
			break;
		}
	}
}
