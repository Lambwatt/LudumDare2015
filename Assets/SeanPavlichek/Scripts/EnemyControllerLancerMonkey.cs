//------------------------------------------------------------------------------------------------
// Author: Sean Pavlichek
// Date: 10/11/2015
// Credit: Christopher Maxwell (Provided basic framework for enemies in the Experiment)
//
// Purpose: This class handles enemy movement in the game.
//------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class EnemyControllerLancerMonkey : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Holds the different pickup types that can be spawned.")]
    GameObject[] pickupTypes;

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
    [Tooltip("This is used to control how long the monkey will attempt to follow the player's movements.")]
    float chaseTime = 3.0f;

    // Denotes possible AI configurations
    private enum AIMode { Normal, Ramming };

    // The AI type of this unit
    private AIMode CurrentAIState;

    // Stores a reference to the player ship script
    public GameObject player;

    // Stores a reference to the player stat handler
    private BalloonData playerData;

    // Stores a reference to the player damage handler
    private BalloonDamager playerDamager;

    // Stores whether the player has been seen
    private bool playerSeen = false;

    // Stores the original y position
    private float startY;

    // Stores the amount of time the monkey has chased the player
    private float timeSpent;

    // Stores whether the monkey is moving up or down in the default movement pattern
    private int dir = 1;

    // Use this for initialization
    void Start()
    {
        // Start in the normal AI state
        CurrentAIState = AIMode.Normal;

        startY = gameObject.transform.position.y;

        // Destroys this unit after a certain period of time
        // Destroy(gameObject, 5.0f);

		player = GameObject.FindGameObjectWithTag("Player");

        if (player != null) // The player ship will be null if the game is over
        {
            playerData = player.GetComponent<BalloonData>();
            playerDamager = player.GetComponent<BalloonDamager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentAIState == AIMode.Normal)
        {
            // Determines what the current AI state should be
            determineAIState();
        }

        switch (CurrentAIState)
        {
            case AIMode.Normal: // If the unit is in normal AI mode
                UpdateNormal();
                break;
            case AIMode.Ramming: // If the unit is in ramming AI mode
                UpdateRamming();
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
        if (dir == 1 && gameObject.transform.position.y < (startY + 2))
        {
            // Change nothing
        }
        else if (dir == 1 && gameObject.transform.position.y >= (startY + 2))
        {
            dir = -1;
        }
        else if (dir == -1 && gameObject.transform.position.y > (startY - 2))
        {
            // Change nothing
        }
        else if (dir == -1 && gameObject.transform.position.y <= (startY - 2))
        {
            dir = 1;
        }

        // Moves the unit
        transform.position += dir * transform.up * Time.deltaTime * moveSpeed;
    }

    /** Handles the update function when the unit is in the Ramming AIMode.
    */
    void UpdateRamming()
    {
        // Moves the unit
        transform.position += transform.forward * Time.deltaTime * ramSpeed;
    }

    /** Determines whether the unit should switch AI modes.
    */
    void determineAIState()
    {
        /**
        // Checks if the player is in range
        // bool canSee = CanSeeTarget("Player");
        Vector3 dirToPlayer = player.transform.position - transform.position;

        // Declares necessary variables in case the parent ship is null
        // Sets them to values that would cause the enemy to use default movement if the player is dead
        float product = -1;
        float angle = 91;

        if (player != null)
        {
            // Normalizes the vector
            Vector3 dirToPlayerNorm = dirToPlayer.normalized;
            // Determines the cosine angle to the target
            product = Vector3.Dot(transform.up, dirToPlayerNorm);
            // Converts the angle to radians
            angle = Mathf.Acos(product);
            // Converts radians to degrees
            angle = angle * Mathf.Rad2Deg;
        }

        if (!playerSeen && (product > 0 && angle < 10)) // Wider angle for aggressive enemies
        {
            // Change the state to SteerTowards
            CurrentAIState = AIMode.Ramming;

            playerSeen = true;

            // Changes the color of the unit
            //GetComponent<Renderer>().material.color = Color.yellow;
        }

        **/

        Vector3 dirToPlayer = player.transform.position - gameObject.transform.position;

        if (dirToPlayer.x < 0 && dirToPlayer.y < -3 && dirToPlayer.y > -7)
        {
            // Change the state to SteerTowards
            CurrentAIState = AIMode.Ramming;

            playerSeen = true;
        }
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
            // Damages the player
            playerDamager.damagePlayer(1);

            // Destroys the enemy
            Destroy(gameObject);
        }
        else if (other.tag == "PlayerProjectile")
        {
            // Adds to the player score
            playerData.ModScore(scoreValue);

            // Destroys the enemy if it encounters a projectile
            Destroy(gameObject);
        }
    }
}
