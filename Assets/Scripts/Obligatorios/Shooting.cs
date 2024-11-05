using UnityEngine;
using Cinemachine;


public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab; // The projectile to instantiate
    [SerializeField] Transform firePoint; // The point from where the projectile is shot
    [SerializeField] float projectileSpeed = 20f; // Speed of the projectile
    [SerializeField] float fireRate = 0.5f; // Time between shots
    private float nextFireTime = 0f;
    private PlayerStats pS;
    private int usedAmmo;
    [SerializeField] float bulletLife = 1;

    private void Start()
    {
        pS = FindObjectOfType<PlayerStats>();
        pS.extraAmmo += Random.Range(50, 150);
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime && Time.timeScale > 0f)
        {
            if (pS != null && pS.ammo > 0)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
                pS.ammo--;
                usedAmmo++;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (pS != null && pS.ammo < 100 && pS.extraAmmo > 0)
            {
                pS.ammo += usedAmmo;
                pS.extraAmmo -= usedAmmo;

                usedAmmo = 0;
            }
        }
    }

    void Shoot()
    {
        // Create a new projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component of the projectile and apply force to it
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * projectileSpeed;

        Destroy(projectile, bulletLife);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R) && currentAmmo <= 999)
    //    {
    //        if (extraAmmo > usedAmmo)
    //        { 
    //            currentAmmo += usedAmmo;
    //            extraAmmo -= usedAmmo;
    //            usedAmmo = 0;
    //        }
    //    }
    //}

    //public void AddAmountAmmo(int number)
    //{
    //    if (currentAmmo < 100)
    //    {
    //        currentAmmo += number;
    //    }
    //    else
    //    {
    //        extraAmmo += number;
    //    }
    //}
}
