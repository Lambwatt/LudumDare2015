using UnityEngine;
using System.Collections;

public class GoToGame : MonoBehaviour {

	public void goToGame(){
		Application.LoadLevel("PlayableBuild");
	}
}
