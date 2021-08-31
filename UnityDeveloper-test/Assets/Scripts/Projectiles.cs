using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        rb.velocity = transform.right * speed;
        rb.angularDrag = 50;
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }
}
