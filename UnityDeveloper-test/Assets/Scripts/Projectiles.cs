using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour, IHit // Projectile
{
    public void DestroyTrigger(GameObject obj)
    {
        Destroy(obj);
        Debug.Log("Destroyed");
    }

    public void OnHit(Collider2D hitInfo)
    {
        if (hitInfo.name != "Projectile(Clone)")
        {
            Debug.Log(hitInfo.name); // Action we need to execute
            DestroyTrigger(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        OnHit(hitInfo);
    }
}
