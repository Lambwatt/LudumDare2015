using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {

	public BalloonData data;

	// Update is called once per frame
	void Update () {
		if(data.dash)
			return;

		transform.position+=new Vector3(0,(data.airContent)*data.maxDelta,0);
		deflate();

		if(transform.position.y>data.maxHeight)
			transform.position = new Vector3(transform.position.x, data.maxHeight, transform.position.z);

		if(transform.position.y<data.minHeight)
			transform.position = new Vector3(transform.position.x, data.minHeight, transform.position.z);
	}

	public void inflate(){
		data.airContent += data.inflatCoefficient;
		if(data.airContent>1.0f)
			data.airContent = 1.0f;
	}

	private void deflate(){
		data.airContent -= data.deflateCoefficient;
		if(data.airContent<-1.0f)
			data.airContent = -1.0f;
	}
	
}
