using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float launchVelocity;
    public float nextFire;
    public float fireRate;
    public float bulletLifetime;

    public GameObject projectilePrefab;
    public Transform firePoint;

    private Vector3 aim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
        {
            Fire();
            nextFire = Time.time + fireRate;
        }
    }

    void Fire()
    {
    //    Camera cam = Camera.main;

     //   Vector3 mousePosition = Input.mousePosition;
     //   aim = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cam.nearClipPlane));
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (launchVelocity, 0, 0));
        Destroy(projectile.gameObject, bulletLifetime);
    }
}
