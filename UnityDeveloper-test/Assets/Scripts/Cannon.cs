using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour // Object A
{
    public Rigidbody2D projectile;

    GameObject enemy; // Object B
    GameObject firePoint; // Object A
    public float shootingRange { get; set; } // Object A detection range
    float projectileWeight; // Mass of projectile
    Vector2 enemyPosition; // Current position of enemy (object B)
    public float cannonAngle { get; set; } // Angle for projectile launch
    //float enemySpeed; // Speed of object B
    Vector2 enemySpeed;
    float projectileSpeed; // Velocity for projectile launch
    float cannonNextShot; // Time for next shot
    public float cannonCooldown { get; set; } // Cooldown object A cannon
    float enemyDist; // Distantion to object B from object A
    public float projectileSpray { get; set; } // Projectile spray modifier

    // Start is called before the first frame update, initialization of our main object A variables
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        firePoint = gameObject;
        shootingRange = 13f;
        projectileWeight = projectile.mass; // Getting projectile mass
        cannonAngle = 45;
        cannonNextShot = Time.time;
        cannonCooldown = 1f;
        StartCoroutine("Shoot");
        projectileSpray = 1;
    }

    IEnumerator Shoot()
    {
        for (; ; )
        {
            enemyPosition = enemy.transform.position;
           // Debug.Log("Enemy Position "+enemyPosition);
            enemyDist = Vector2.Distance(enemy.transform.position, firePoint.transform.position); // Getting object B current position
            if (enemyDist <= shootingRange)
            {
                if (Time.time > cannonNextShot)
                {
                    enemySpeed = enemy.GetComponent<Rigidbody2D>().velocity;
                    firePoint.transform.rotation = Quaternion.Euler(0, 0, cannonAngle); // Setting our cannon angle
                    Debug.Log("Enemy Velocity "+enemySpeed);
                    float projectileSpeedX = VelocityCalc(enemyPosition, cannonAngle, enemySpeed.x).x;
                    float projectileSpeedY = VelocityCalc(enemyPosition, cannonAngle, enemySpeed.y).y;
                    projectileSpeed = Mathf.Sqrt((projectileSpeedX * projectileSpeedX) + (projectileSpeedY * projectileSpeedY));
                    Rigidbody2D p = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
                    p.velocity = transform.right * projectileSpeed; // setting our 
                    cannonNextShot = Time.time + cannonCooldown;
                }
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    // Function for finding optimal velocity of projectile
    Vector2 VelocityCalc(Vector2 destination, float angle, float enemySpeed)
    {
        Vector2 dir = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y);
        float height = dir.y;
        dir.y = 0;
        float dist = dir.magnitude;
        float a = angle * Mathf.Deg2Rad;
        dir.y = dist * Mathf.Tan(a);
        dist += height / Mathf.Tan(a);
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        float time = (velocity * 2 * Mathf.Sin(a*1.8f)) / Physics2D.gravity.y;
        dir = new Vector2((destination.x - enemySpeed*time)*Random.Range(1, projectileSpray) - transform.position.x, destination.y - transform.position.y);
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
