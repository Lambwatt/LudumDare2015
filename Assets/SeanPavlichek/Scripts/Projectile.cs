using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    [SerializeField]
    [Tooltip("The speed of the projectile")]
    private float speed;

    // Use this for initialization

    void Start () {
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update() {
        gameObject.transform.Translate(speed, 0, 0);
    }

    // Called when the bullet collides with something
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            // Code for what happens to the player
            Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}