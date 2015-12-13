//------------------------------------------------------------------------------------------------
// Author: Sean Pavlichek
// Date: 10/11/2015
// Credit: Christopher Maxwell (Provided basic framework for enemies in the Experiment)
//
// Purpose: This class handles enemy movement in the game.
//------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class EnemyControllerBasic : MonoBehaviour {
    //[SerializeField]
    //[Tooltip("Holds the different pickup types that can be spawned.")]
    //GameObject[] pickupTypes;

    [SerializeField]
    [Tooltip("This is used to control how fast the enemy moves.")]
    float moveSpeed = 5.0f;

    [SerializeField]
    [Tooltip("This is used to control how fast the enemy moves to attempt to ram the player when in range.")]
    float ramSpeed = 5.0f;

    [SerializeField]
    [Tooltip("This is used to control how many points the enemy is worth.")]
    int scoreValue = 50;

    [SerializeField]
    [Tooltip("This is used to control whether the enemy should be aggressive.")]
    bool aggressive;

    // Denotes possible AI configurations
    private enum AIMode { Normal, Ramming, SteerTowards };

    // The AI type of this unit
    private AIMode CurrentAIState;

    // Stores a reference to the player ship script
    public GameObject player;

    // Use this for initialization
    void Start () {
        // Start in the normal AI state
        CurrentAIState = AIMode.Normal;

        // Destroys this unit after a certain period of time
        Destroy(gameObject, 5.0f);

        // Locates the player ship and assigns it as the parent
        //GameObject playerShip = GameObject.Find("PlayerShip");
        //if (playerShip != null) // The player ship will be null if the game is over
        //{
            //parentShip = playerShip.GetComponent<ShipPlayerController>();
        //}
    }
	
	// Update is called once per frame
	void Update () {
        // Determines what the current AI state should be
        determineAIState();

	    switch (CurrentAIState)
        {
            case AIMode.Normal: // If the unit is in normal AI mode
                UpdateNormal();
                break;
            case AIMode.Ramming: // If the unit is in ramming AI mode
                UpdateRamming();
                break;
            case AIMode.SteerTowards: // If the unit is steering towards the player
                UpdateSteerTowards();
                break;
            default: // Returns an error if an invalid state was found
                Debug.Log("Unknown AI State " + CurrentAIState);
                break;
        }
	}

    /** Handles the update function when the unit is in the Normal AIMode.
    */
    void UpdateNormal()
    {
        // Moves the unit
        transform.position += transform.up * Time.deltaTime * moveSpeed;
    }

    /** Handles the update function when the unit is in the Ramming AIMode.
    */
    void UpdateRamming()
    {
        // Moves the unit
        transform.position += transform.up * Time.deltaTime * ramSpeed;
    }

    /** Handles the update function when the unit is in the Ramming AIMode.
    */
    void UpdateSteerTowards()
    {
        // Finds the direction to the target
        Vector3 dirToPlayer = player.transform.position - transform.position;

        // Rotates the unit toward the target
        transform.up = Vector3.RotateTowards(transform.up, dirToPlayer, Time.deltaTime * ramSpeed, 0.0f);

        // Moves the unit toward the target
        UpdateNormal();
    }

    /** Determines whether the unit should switch AI modes.
    */
    void determineAIState()
    {
        // Checks if the player is in range
        bool canSee = CanSeeTarget("Player");

        // Declares necessary variables in case the parent ship is null
        // Sets them to values that would cause the enemy to use default movement if the player is dead
        float product = -1;
        float angle = 91;

        if (player != null)
        {
            // Finds the direction to the target
            Vector3 dirToPlayer = player.transform.position - transform.position;
            // Normalizes the vector
            Vector3 dirToPlayerNorm = dirToPlayer.normalized;
            // Determines the cosine angle to the target
            product = Vector3.Dot(transform.up, dirToPlayerNorm);
            // Converts the angle to radians
            angle = Mathf.Acos(product);
            // Converts radians to degrees
            angle = angle * Mathf.Rad2Deg;
        }

        if (canSee) // If the player is in range, begin ramming mode
        {
            // Changes the mode to Ramming
            CurrentAIState = AIMode.Ramming;

            // Changes the color of the unit
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (aggressive && (product > 0 && angle < 90)) // Wider angle for aggressive enemies
        {
            // Change the state to SteerTowards
            CurrentAIState = AIMode.SteerTowards;

            // Changes the color of the unit
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (!aggressive && (product > 0 && angle < 40))
        {
            // Change the state to SteerTowards
            CurrentAIState = AIMode.SteerTowards;

            // Changes the color of the unit
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            if (aggressive) // If the enemy is not supposed to return to normal mode, continue ramming
            {
                // Changes the mode to Ramming
                CurrentAIState = AIMode.Ramming;

                // Changes the color of the unit
                GetComponent<Renderer>().material.color = Color.red;
            } else // If this is a normal enemy, resume normal mode
            {
                // Changes the mode to Normal
                CurrentAIState = AIMode.Normal;

                // Changes the color back to normal
                GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    /** Checks if this unit can see the target
    */
    bool CanSeeTarget(string target)
    {
        // Stores the units hit by the raycast
        RaycastHit hitInfo;

        // Search for targets
        bool hitAny = Physics.Raycast(transform.position, transform.up, out hitInfo);

        if (hitAny) // If anything was hit
        {
            if (hitInfo.collider.gameObject.tag == target) // Checks if the collider seen is the target
            {
                return true;
            }
        }

        // Default case
        return false;
    }

    /** Handles pickup spawning.
    */
    public void spawnPickup()
    {
        // Randomly selects the pickup type
        //int type = Random.Range(0, pickupTypes.Length);

        // Creates the new pickup
        //Instantiate(pickupTypes[type], gameObject.transform.position, new Quaternion(0,0,0,0));
    }

    /** Handles collision.
    */
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Destroys the enemy ship
            Destroy(gameObject);
        } else if (other.tag == "PlayerProjectile")
        {
            // Adds to the player score
            //parentShip.ModScore(scoreValue);

            // Modifies the player's hits
            //parentShip.ModEnemyHits(1);

            // Randomly generates a pickup
            int pickupChance = Random.Range(0, 100);
            if (pickupChance < 50)
            {
                spawnPickup();
            }

            // Destroys the enemy if it encounters a projectile
            Destroy(gameObject);
        }
        else if (other.tag == "PlayerMissile")
        {
            // Adds to the player score
            //parentShip.ModScore(scoreValue);

            // Modifies the player's hits
            //parentShip.ModEnemyHits(1);

            // Randomly generates a pickup
            int pickupChance = Random.Range(0, 100);
            if (pickupChance < 50)
            {
                spawnPickup();
            }

            // Destroys the enemy if it encounters a projectile
            Destroy(gameObject);
        }
    }
}
