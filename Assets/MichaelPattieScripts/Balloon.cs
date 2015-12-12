using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {

	public float inflatCoefficient;
	public float deflateCoefficient;
	public float airContent = 0.1f;
	public float maxHeight;
	public float minHeight;
	public float maxDelta;

	// Update is called once per frame
	void Update () {
		transform.position+=new Vector3((airContent)*maxDelta,0,0);
		deflate();

		if(transform.position.y>maxHeight)
			transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);

		if(transform.position.y<minHeight)
			transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
	}

	public void inflate(){
		airContent += inflatCoefficient;
		if(airContent>1.0f)
			airContent = 1.0f;
	}

	private void deflate(){
		airContent -= deflateCoefficient;
		if(airContent<-1.0f)
			airContent = -1.0f;
	}
	
}
