using UnityEngine;
using System.Collections;

public class TurretFiring : MonoBehaviour {
    [SerializeField]
    [Tooltip("The amount of time between shots.")]
    private float fireRate;

    private float fireTimer;

    [SerializeField]
    [Tooltip("The projectile to instantiate.")]
    private GameObject Bullet;

    [SerializeField]
    [Tooltip("The animation for the explosion.")]
    private GameObject Explosion;

    [SerializeField]
    [Tooltip("Where bullets are fired from.")]
    private GameObject firePosition;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(15, 0, 0);

        fireTimer += Time.deltaTime;

        if (fireTimer > fireRate)
        {
            fireTimer = 0;
            Instantiate(Bullet, firePosition.transform.position, gameObject.transform.rotation);
            Instantiate(Explosion, firePosition.transform.position, gameObject.transform.rotation);
        }
    }
}
