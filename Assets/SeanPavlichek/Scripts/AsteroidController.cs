//------------------------------------------------------------------------------------------------
// Author: Sean Pavlichek
// Date: 10/11/2015
// Credit: Christopher Maxwell (Provided basic framework for enemies in the Experiment)
//
// Purpose: This class handles enemy movement in the game.
//------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {
    [SerializeField]
    [Tooltip("Holds the different pickup types that can be spawned.")]
    GameObject[] pickupTypes;

    // Enumerates allowable asteroid types
    enum Asteroid { BIG, SMALL}

    [SerializeField]
    [Tooltip("Holds the type of the asteroid.")]
    Asteroid type;

    [SerializeField]
    [Tooltip("The small asteroid type.")]
    GameObject smallAsteroid;

    [SerializeField]
    [Tooltip("Determines the force of movement behind the asteroid.")]
    float moveForce = 10.0f;

    // Stores a reference to the player ship script
    private ShipPlayerController parentShip;

    // Use this for initialization
    void Start () {
        // Sets the movement force of the asteroid
        float yForce = Random.Range(0, moveForce);
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, yForce, 0));

        Destroy(gameObject, 10);
    }
	
	// Update is called once per frame
	void Update () {

	}

    /** Handles pickup spawning.
    */
    public void spawnPickup()
    {
        // Randomly selects the pickup type
        int type = Random.Range(0, pickupTypes.Length);

        // Creates the new pickup
        Instantiate(pickupTypes[type], gameObject.transform.position, new Quaternion(0,0,0,0));
    }

    /** Handles collision.
    */
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Destroys the asteroid
            Destroy(gameObject);
        } else if (other.tag == "PlayerProjectile" || other.tag == "PlayerMissile")
        {
            if (type == Asteroid.SMALL)
            {
                // Randomly generates a pickup
                int pickupChance = Random.Range(0, 100);
                if (pickupChance < 50)
                {
                    spawnPickup();
                }
            } else
            {
                // Spawns more asteroids
                Instantiate(smallAsteroid, gameObject.transform.position, gameObject.transform.rotation);
                Instantiate(smallAsteroid, gameObject.transform.position, gameObject.transform.rotation);
            }
            
            // Destroys the asteroid if it encounters a projectile
            Destroy(gameObject);
        }
    }
}
