using UnityEngine;
using System.Collections;

public class BalloonData : MonoBehaviour {

	public float speed;
	public float inflatCoefficient;
	public float deflateCoefficient;
	public float airContent = 0.1f;
	public float maxHeight;
	public float minHeight;
	public float maxDelta;
	public float speedBoost;
	public float shotCooldown;

	[HideInInspector] public bool dash = false;
	[HideInInspector] public bool shooting = false;
	[HideInInspector] public bool shotReady = true;
	[HideInInspector] public float currentShotCooldown = 0.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
