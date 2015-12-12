using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject projectile;
	public float barrelLength;
	public BalloonData data;
	
	// Update is called once per frame
	void Update () {
		if(data.shotReady){
			if(data.shooting){
				Instantiate(projectile, transform.position + new Vector3(barrelLength,0,0), Quaternion.identity);
				data.shotReady = false;
				data.currentShotCooldown = data.shotCooldown;
			}
		}else{
			data.currentShotCooldown -= Time.deltaTime;
			if(data.currentShotCooldown < 0){
				data.currentShotCooldown = 0;
				data.shotReady  = true;
			}
		}
	}
}
