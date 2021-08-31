using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectilePrefab;

    GameObject enemy;
    GameObject tower;
    Transform firePoint;
    float shootingRange;

    // Update is called once per frame
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        tower = GameObject.FindGameObjectWithTag("Tower");
        firePoint = gameObject.transform.Find("FirePoint");
        shootingRange = 13f;
        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        for (; ; )
        {
            Debug.Log(Vector2.Distance(enemy.transform.position,tower.transform.position));
            if (Vector2.Distance(enemy.transform.position, tower.transform.position) <= shootingRange)
            { 
                Instantiate(projectilePrefab, firePoint.position, firePoint.rotation); 
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}
