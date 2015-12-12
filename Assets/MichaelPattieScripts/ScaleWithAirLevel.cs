using UnityEngine;
using System.Collections;

public class ScaleWithAirLevel : MonoBehaviour {

	public BalloonData data;
	public float scaleFactor;
	//public Transform basket;
	//public float riseFactor;
	private Vector3 basePosition;
	private Vector3 baseScale;


	// Use this for initialization
	void Start () {
		basePosition = transform.localPosition;
		baseScale = transform.localScale;

	}
	
	// Update is called once per frame
	void Update () {
		float scaleChange = scaleFactor*(data.airContent+1.0f);
		transform.localScale = baseScale + new Vector3(scaleChange, scaleChange, scaleChange);
		transform.localPosition = basePosition + new Vector3(0, scaleChange/2.0f, 0);
	}


}
