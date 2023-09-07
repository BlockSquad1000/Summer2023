using Photon.Pun;
using System.Collections;

using UnityEngine;
using TMPro;

public class PlayerShoot : MonoBehaviourPun
{
    public float launchVelocity;
    public float nextFire;
    public float fireRate;
    public float bulletLifetime;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public CharactersSO playerProperties;

    private Vector3 aim;

   // public bool primary;
   // public bool secondary;

    private int currentAmmo;
    public int ammoReplenishAmount;

    private bool firing = false;
    private bool canFire = true;

    private void Start()
    {
        currentAmmo = playerProperties.ammoCapacity;
    }

    private void FixedUpdate()
    {
        DeathmatchUIManager.Instance.ammoText.text = currentAmmo.ToString();
        if (!photonView.IsMine) return;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            photonView.RPC("NotifyFire", RpcTarget.AllBuffered);
        }
        else
        {
            photonView.RPC("CeaseFire", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void NotifyFire()
    {
        if(!firing && canFire)
        {
            firing = true;
            canFire = false;
            StartCoroutine(Fire());
        }
    }

    [PunRPC]
    public void CeaseFire()
    {
        firing = false;
    }

    private IEnumerator Fire()
    {
        while (firing)
        {
            if(currentAmmo > 0)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(launchVelocity, 0, 0));
                Destroy(projectile.gameObject, bulletLifetime);
                currentAmmo--;
            }

            yield return new WaitForSeconds(playerProperties.rateOfFire);
        }

        canFire = true;
    }

    /*void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire && primary)
        {
            Fire();
            nextFire = Time.time + fireRate;
        }

        if (Input.GetKey(KeyCode.Mouse1) && Time.time > nextFire && secondary)
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
    }*/

    public void ReplenishAmmo()
    {
        if(currentAmmo <= playerProperties.ammoCapacity)
        {
            currentAmmo += ammoReplenishAmount;
            if(currentAmmo >= playerProperties.ammoCapacity)
            {
                currentAmmo = playerProperties.ammoCapacity;
            }
        }
    }
}
    
