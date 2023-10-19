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

    [SerializeField] private bool primaryFire = false;
    [SerializeField] private bool secondaryFire = false;

    private bool isLaser = false;
  //  private LineRenderer lineRenderer;

    public AudioSource weaponAudio;
    public AudioClip[] firingSounds;
    public AudioClip currentClip;

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

            primaryFire = true;
            secondaryFire = false;
        }
        else
        {
            photonView.RPC("CeaseFire", RpcTarget.AllBuffered);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            photonView.RPC("NotifyFire", RpcTarget.AllBuffered);

            primaryFire = false;
            secondaryFire = true;
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
            if (primaryFire)
            {
                if (currentAmmo > 0)
                {

                    GameObject go = Instantiate(projectilePrefab, firePoint);
                    go.GetComponent<ProjectileDamage>().Initialize(transform.forward, playerProperties.projectileSpeed, playerProperties.weaponDamage);
                    currentClip = firingSounds[0];
                    weaponAudio.clip = currentClip;
                    weaponAudio.Play();
                    currentAmmo--;
                    Debug.Log("Currently firing Primary projectiles.");
                }
            }

            if (secondaryFire)
            {
                if (currentAmmo > 0)
                {
                    GameObject go = Instantiate(projectilePrefab, firePoint);
                    go.GetComponent<ProjectileDamage>().Initialize(transform.forward, playerProperties.projectileSpeed, playerProperties.weaponDamage);
                    currentClip = firingSounds[1];
                    weaponAudio.clip = currentClip;
                    weaponAudio.Play();
                    currentAmmo--;
                    Debug.Log("Currently firing Secondary projectiles.");
                }
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
    
