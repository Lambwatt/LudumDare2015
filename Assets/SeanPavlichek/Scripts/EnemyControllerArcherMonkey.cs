using UnityEngine;
using System.Collections;

public class EnemyControllerArcherMonkey : MonoBehaviour {
    [SerializeField]
    [Tooltip("Holds the different pickup types that can be spawned.")]
    GameObject[] pickupTypes;

    [SerializeField]
    [Tooltip("This is used to control how fast the enemy moves.")]
    float moveSpeed = 5.0f;

    [SerializeField]
    [Tooltip("This is used to control how many points the enemy is worth.")]
    int scoreValue = 50;

    [SerializeField]
    [Tooltip("Where arrows are fired from.")]
    private GameObject firePosition;

    [SerializeField]
    [Tooltip("The projectile to instantiate.")]
    private GameObject arrow;

    [SerializeField]
    [Tooltip("The amount of time between shots.")]
    private float fireRate;

    [SerializeField]
    [Tooltip("The maximum degree of variation in shot angle (higher number = more inaccurate).")]
    [Range(0, 45.0f)]
    private float fireError;

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

    private float fireTimer;

    // Use this for initialization
    void Start()
    {
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
        UpdateNormal();

        fireTimer += Time.deltaTime;

        if (fireTimer > fireRate)
        {
            fireTimer = 0;
            GameObject newArrow;
            newArrow = Instantiate(arrow, firePosition.transform.position, firePosition.transform.rotation) as GameObject;
            newArrow.transform.Rotate(Random.Range(-fireError, fireError), 0, 0);
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
