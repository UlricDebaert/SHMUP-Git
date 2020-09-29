using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Transform firePoint1;
    public Transform firePoint2;

    public GameObject bulletPrefab;

    public float bulletForce;
    public float bulletFireRate;
    float fireRateTimer;

    private bool canShoot;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && canShoot)
        {
            Shoot();
            canShoot = false;
            fireRateTimer = bulletFireRate;
        }

        if (!canShoot)
        {
            fireRateTimer -= Time.deltaTime;
            if (fireRateTimer < 0.0f)
            {
                canShoot = true;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint1.up * bulletForce, ForceMode2D.Impulse);
        
        GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        rb2.AddForce(firePoint2.up * bulletForce, ForceMode2D.Impulse);
    }
}
