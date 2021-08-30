using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public Transform firePoint;
    public GameObject projectilePrefab;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        for (; ; )
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(.1f);
        }
    }
}
