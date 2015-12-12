//------------------------------------------------------------------------------------------------
// Author: Sean Pavlichek
// Date: 10/11/2015
// Credit: Christopher Maxwell (Provided basic framework for enemies in the Experiment)
//
// Purpose: This class handles enemy fighter movement in the game.
//------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class EnemyControllerFighter : MonoBehaviour {
    [SerializeField]
    [Tooltip("Holds the different pickup types that can be spawned.")]
    GameObject[] pickupTypes;

    [SerializeField]
    [Tooltip("This holds the projectile that is being fired.")]
    Projectile bullet;

    [SerializeField]
    [Tooltip("This is used to control how fast the enemy moves.")]
    float moveSpeed = 2.0f;

    [SerializeField]
    [Tooltip("This is used to control how fast the enemy moves side to side.")]
    float moveSpeedHoriz = 5.0f;

    [SerializeField]
    [Tooltip("This is used to control how fast the enemy shoots.")]
    float maxShotTime = 2.0f;

    [SerializeField]
    [Tooltip("This is used to control how many points the enemy is worth.")]
    int scoreValue = 100;

    [SerializeField]
    [Tooltip("This is used to control the strength of the enemy's shields.")]
    [Range(25, 200)]
    int shieldMax = 100;

    [SerializeField]
    [Tooltip("This is used to control how much shield damage this ship takes from hits.")]
    [Range(1, 100)]
    int hitDamage = 25;

    // Stores a reference to the player ship script
    private ShipPlayerController parentShip;

    // A timer for the sinusoidal movement of the ship
    private float sinTimer = 0;

    // A timer for the ship's cannons
    private float bulletTimer = 0;

    // Stores the amount of shields the ship has left
    private int shields;

    // Use this for initialization
    void Start()
    {
        shields = shieldMax;
        // Locates the player ship and assigns it as the parent
        GameObject playerShip = GameObject.Find("PlayerShip");
        if (playerShip != null) // The player ship will be null if the game is over
        {
            parentShip = playerShip.GetComponent<ShipPlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Increments the timers
        sinTimer += Time.deltaTime;
        bulletTimer += Time.deltaTime;

        // Resets the timer if it goes over 360
        if (sinTimer > 360)
        {
            sinTimer = 0;
        }

        // If the cannons are ready to fire
        if (bulletTimer > maxShotTime)
        {
            // Fire the cannons
            doWeaponFire();
            // Reset the timer
            bulletTimer = 0;
        }

        // Moves the ship
        transform.position += transform.up * Time.deltaTime * moveSpeed;
        transform.position += transform.right * Time.deltaTime * Mathf.Sin(sinTimer) * moveSpeedHoriz;
    }

    /** Handles pickup spawning.
    */
    public void spawnPickup()
    {
        // Randomly selects the pickup type
        int type = Random.Range(0, pickupTypes.Length);

        // Creates the new pickup
        Instantiate(pickupTypes[type], gameObject.transform.position, new Quaternion(0, 0, 0, 0));
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
            shields -= hitDamage;

            // Modifies the player's hits
            parentShip.ModEnemyHits(1);

            if (shields <= 0)
            {
                // Adds to the player score
                parentShip.ModScore(scoreValue);

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
        else if (other.tag == "PlayerMissile")
        {
            // More damage is done if the multiplier is increased for missiles
            shields -= hitDamage * (other.GetComponent<Projectile>().damageMult);

            // Modifies the player's hits
            parentShip.ModEnemyHits(1);

            if (shields <= 0)
            {
                // Adds to the player score
                parentShip.ModScore(scoreValue);

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

    void doWeaponFire()
    {
        // Fires the weapon by instantiating a bullet object
        Projectile newBullet = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation) as Projectile;

        newBullet.setType(3);
    }

    /** Destroys this type of ship when it becomes invisible.
    */
    void OnBecameInvisible()
    {
        // Destroys the game object
        Destroy(gameObject, 0.25f);
    }
}
