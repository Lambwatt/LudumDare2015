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
	public int health = 100;

	public float speedPower;
	public float inflatCoefficientPower;
	public float deflateCoefficientPower;
	public float speedUpPower;
	public float slowDownPower;

	[HideInInspector]public bool speedPowerOn;
	[HideInInspector]public bool inflatCoefficientPowerOn;
	[HideInInspector]public bool deflateCoefficientPowerOn;
	[HideInInspector]public bool speedUpPowerOn;
	[HideInInspector]public bool slowDownPowerOn;
	[HideInInspector]public bool immunityOn;

	[HideInInspector] public bool dash = false;
	[HideInInspector] public bool shooting = false;
	[HideInInspector] public bool shotReady = true;
	[HideInInspector] public float currentShotCooldown = 0.0f;
	[HideInInspector] public string objectHittingBalloon;
	[HideInInspector] public string objectHittingBasket;
	[HideInInspector] public int score = 0;

	public void ModScore(int change){
		score += change;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(health<0){

			Application.LoadLevel("GameOver");
			
		}
	}

	public void cancelPowerups(){
		speedPowerOn = false;
		inflatCoefficientPowerOn = false;
		deflateCoefficientPowerOn = false;
		speedUpPowerOn = false;
		slowDownPowerOn = false;
		immunityOn = false;
	}
}
