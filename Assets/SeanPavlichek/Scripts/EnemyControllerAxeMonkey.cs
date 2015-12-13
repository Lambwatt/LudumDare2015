//------------------------------------------------------------------------------------------------
// Author: Sean Pavlichek
// Date: 10/11/2015
// Credit: Christopher Maxwell (Provided basic framework for enemies in the Experiment)
//
// Purpose: This class handles enemy movement in the game.
//------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class EnemyControllerAxeMonkey : MonoBehaviour
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
    private enum AIMode { Normal, Ramming, SteerTowards };

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

        if (player != null) // The player ship will be null if the game is over
        {
            playerData = player.GetComponent<BalloonData>();
            playerDamager = player.GetComponent<BalloonDamager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentAIState != AIMode.Ramming)
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
        if (playerSeen)
        {
            // Moves the unit
            transform.position += transform.right * Time.deltaTime * moveSpeed;
        } else
        {
            if (dir == 1 && gameObject.transform.position.y < (startY + 5))
            {
                // Change nothing
            }
            else if (dir == 1 && gameObject.transform.position.y >= (startY + 5))
            {
                dir = -1;
            }
            else if (dir == -1 && gameObject.transform.position.y > (startY - 5))
            {
                // Change nothing
            }
            else if (dir == -1 && gameObject.transform.position.y <= (startY - 5))
            {
                dir = 1;
            }

            // Moves the unit
            transform.position -= dir * transform.up * Time.deltaTime * moveSpeed;
        }
    }

    /** Handles the update function when the unit is in the Ramming AIMode.
    */
    void UpdateRamming()
    {
        // Moves the unit
        transform.position += transform.right * Time.deltaTime * ramSpeed;
    }

    /** Handles the update function when the unit is in the Ramming AIMode.
    */
    void UpdateSteerTowards()
    {
        // Finds the direction to the target
        Vector3 dirToPlayer = player.transform.position - transform.position;

        // Rotates the unit toward the target
        transform.right = Vector3.RotateTowards(transform.right, dirToPlayer, Time.deltaTime * ramSpeed, 0.0f);

        // Updates chase time
        timeSpent += Time.deltaTime;

        // Moves the unit toward the target
        UpdateNormal();
    }

    /** Determines whether the unit should switch AI modes.
    */
    void determineAIState()
    {
        // Checks if the player is in range
        // bool canSee = CanSeeTarget("Player");
        bool canSee = false;
        Vector3 dirToPlayer = player.transform.position - transform.position;

        if (Mathf.Sqrt(dirToPlayer.y * dirToPlayer.y + dirToPlayer.x * dirToPlayer.x) < 10)
        {
            canSee = true;
        }

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

        if (!playerSeen && canSee) // Wider angle for aggressive enemies
        {
            // Change the state to SteerTowards
            CurrentAIState = AIMode.SteerTowards;

            playerSeen = true;

            // Changes the color of the unit
            //GetComponent<Renderer>().material.color = Color.yellow;
        } else if (playerSeen && timeSpent >= chaseTime) // If the player is in range, begin ramming mode
        {
            // Changes the mode to Ramming
            CurrentAIState = AIMode.Ramming;

            // Changes the color of the unit
            //GetComponent<Renderer>().material.color = Color.red;
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
            // Destroys the enemy ship
            Destroy(gameObject);
        }
        else if (other.tag == "PlayerProjectile")
        {
            // Adds to the player score
            //playerData.ModScore(scoreValue);

            // Destroys the enemy if it encounters a projectile
            Destroy(gameObject);
        }
    }
}
