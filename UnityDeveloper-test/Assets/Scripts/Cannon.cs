using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour // Object A
{
    public GameObject projectile;

    Enemy en;
    Rigidbody2D proj;
    GameObject enemy; // Object B
    GameObject firePoint; // Object A
    public float shootingRange { get; set; } // Object A detection range
    float projectileWeight; // Mass of projectile
    Vector2 enemyPosition; // Current position of enemy (object B)
    public float cannonAngle { get; set; } // Angle for projectile launch
    Vector2 enemySpeed; // Speed of object B
    float projectileSpeed; // Velocity for projectile launch
    float cannonNextShot; // Time for next shot
    public float cannonCooldown { get; set; } // Cooldown object A cannon
    float enemyDist; // Distantion to object B from object A
    public float projectileSpray { get; set; } // Projectile spray modifier

    // Start is called before the first frame update, initialization of our main object A variables
    void Start()
    {
        proj = projectile.GetComponent<Rigidbody2D>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        en = enemy.GetComponent<Enemy>();
        firePoint = gameObject;
        projectileWeight = proj.mass;
        cannonNextShot = Time.time;
        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        for (; ; )
        {
            enemyPosition = enemy.transform.position;
            enemyDist = Vector2.Distance(enemy.transform.position, firePoint.transform.position); // Getting object B current position
            if (enemyDist <= shootingRange)
            {
                if (Time.time > cannonNextShot)
                {
                    enemyPosition = enemy.transform.position;
                    enemySpeed = enemy.GetComponent<Rigidbody2D>().velocity;
                    firePoint.transform.rotation = Quaternion.Euler(0, 0, cannonAngle); // Setting our cannon angle
                    float projectileSpeedX = VelocityCalc(enemyPosition, cannonAngle, enemySpeed.x, projectileWeight, en.MoveRight).x;
                    float projectileSpeedY = VelocityCalc(enemyPosition, cannonAngle, enemySpeed.y, projectileWeight, en.MoveRight).y;
                    projectileSpeed = Mathf.Sqrt((projectileSpeedX * projectileSpeedX) + (projectileSpeedY * projectileSpeedY));
                    Rigidbody2D p = Instantiate(proj, firePoint.transform.position, firePoint.transform.rotation);
                    p.velocity = transform.right * projectileSpeed; // setting our 
                    cannonNextShot = Time.time + cannonCooldown;
                }
            }
            yield return new WaitForSeconds(.01f);
        }
    }

    // Function for finding optimal velocity of projectile
    Vector2 VelocityCalc(Vector2 destination, float angle, float enemySpeed, float projectileMass, bool MoveRight)
    {
        Vector2 dir = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y);
        float height = dir.y;
        dir.y = 0;
        float dist = dir.magnitude;
        float a = angle * Mathf.Deg2Rad;
        dir.y = dist * Mathf.Tan(a);
        dist += height / Mathf.Tan(a);
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        float time = (velocity * 2 * Mathf.Sin(a*2f)) / (Physics2D.gravity.y);
        if (MoveRight)
        {
            dir = new Vector2((destination.x - enemySpeed* Mathf.Tan(a*1.5f) * time) * Random.Range(1, projectileSpray) - transform.position.x, destination.y - transform.position.y);
        }
        else
        {
            dir = new Vector2((destination.x - enemySpeed * Mathf.Tan(a) * time) * Random.Range(1, projectileSpray) - transform.position.x, destination.y - transform.position.y);
        }
        height = dir.y;
        dir.y = 0;
        dist = dir.magnitude;
        a = angle * Mathf.Deg2Rad;
        dir.y = dist * Mathf.Tan(a);
        dist += height / Mathf.Tan(a);
        velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * dir.normalized;
    }
}
