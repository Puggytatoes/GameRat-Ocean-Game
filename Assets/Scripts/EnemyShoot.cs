using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [SerializeField]
    GameObject projectile;

    float fireRate;
    float nextFire;





    void Start()
    {
        fireRate = 1f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
