using Photon.Pun;
using System.Collections;

using UnityEngine;
using TMPro;

public class PlayerShoot : MonoBehaviourPun
{

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

    private bool isLaser = false;
  //  private LineRenderer lineRenderer;

    public AudioSource weaponSound;

    private void Start()
    {
        MaxAmmo();

      /*  isLaser = playerProperties.weaponName == "Laser Beam";
        if (isLaser)
        {
            lineRenderer = GetComponentInChildren<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
        }*/
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
            Debug.Log("Notifying fire");
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
               /* if (isLaser)
                {
                    Ray laser = new Ray(firePoint.position, transform.forward * 200f);
                    if(Physics.Raycast(laser, out RaycastHit hit))
                    {
                        lineRenderer.enabled = true;
                        lineRenderer.SetPosition(0, firePoint.position);
                        lineRenderer.SetPosition(1, hit.point);
                        weaponSound.Play();
                        currentAmmo--;

                        StartCoroutine(DisableLaser());

                        GameObject go = hit.collider.gameObject;
                        if(go.CompareTag("Player") && go.GetComponent<PhotonView>().IsMine)
                        {
                            go.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.AllBuffered, playerProperties.weaponDamage);
                        }
                    }
                }*/
               // else
              //  {
                    GameObject go = Instantiate(projectilePrefab, firePoint);
                    go.GetComponent<ProjectileDamage>().Initialize(transform.forward, playerProperties.projectileSpeed, playerProperties.weaponDamage);
                    //weaponSound.Play();
                    currentAmmo--;
                    Debug.Log("Currently firing projectiles.");
            //    }
                
            }

            yield return new WaitForSeconds(playerProperties.rateOfFire);
        }

        canFire = true;
    }

   /* private IEnumerator DisableLaser()
    {
        yield return new WaitForSeconds(playerProperties.rateOfFire * 0.5f);
        lineRenderer.enabled = false;
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

    public void MaxAmmo()
    {
        currentAmmo = playerProperties.ammoCapacity;
    }
}
    
