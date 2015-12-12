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

    private Vector3 firePosition;

    // Use this for initialization
    void Start()
    {
        firePosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(15, 0, 0);

        fireTimer += Time.deltaTime;

        if (fireTimer > fireRate)
        {
            Instantiate(Bullet, firePosition, gameObject.transform.rotation);
            Instantiate(Explosion, firePosition, gameObject.transform.rotation);
            fireTimer = 0;
        }
    }
}
