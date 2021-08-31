using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour // Projectile
{
    Rigidbody2D projectile;
    public float projectileMass { get; private set; }

    private void Start()
    {
        projectile = gameObject.GetComponent<Rigidbody2D>();
        projectileMass = projectile.mass;
    }

    // On projectile collide event trigger
    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        if (hitInfo.name != "Projectile(Clone)")
        {
            Debug.Log(hitInfo.name); // Action we need to execute
            Destroy(gameObject); // Destroying of object
        }
    }
}
