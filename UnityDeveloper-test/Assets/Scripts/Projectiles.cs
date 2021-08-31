using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour // Projectile
{
    // On projectile collide event trigger
    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name); // Action we need to execute
        Destroy(gameObject); // Destroying of object
    }
}
