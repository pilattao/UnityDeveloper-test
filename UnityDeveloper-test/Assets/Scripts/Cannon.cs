using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour // Object A
{
    public Rigidbody2D projectile;

    GameObject enemy; // Object B
    GameObject firePoint; // Object A
    float shootingRange; // Object A detection range
    float projectileWeight; // Mass of projectile
    float enemyPosition; // Current position of enemy (object B)
    float cannonAngle; // Angle for projectile launch
    float enemySpeed; // Velocity of object B
    float projectileSpeed; // Velocity for projectile launch
    float cannonNextShot; // Time for next shot
    float cannonCooldown; // Cooldown object A cannon

    // Start is called before the first frame update, initialization of our main object A variables
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        firePoint = gameObject;
        shootingRange = 13f;
        projectileWeight = projectile.mass; // Getting projectile mass
        cannonAngle = 45; //TMP
        projectileSpeed = 5; //TMP
        cannonNextShot = Time.time;
        cannonCooldown = 1f;
        enemySpeed = enemy.GetComponent<Rigidbody2D>().velocity.magnitude;
        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        for (; ; )
        {
            enemyPosition = Vector2.Distance(enemy.transform.position, firePoint.transform.position); // Getting object B current position
            if (enemyPosition <= shootingRange)
            {
                if (Time.time > cannonNextShot)
                {
                    firePoint.transform.rotation = Quaternion.Euler(0, 0, cannonAngle); // Setting our cannon angle
                    Rigidbody2D p = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
                    p.velocity = transform.right * projectileSpeed; // setting our 
                    cannonNextShot = Time.time + cannonCooldown;
                }
            }
            yield return new WaitForSeconds(.01f);
        }
    }
}
