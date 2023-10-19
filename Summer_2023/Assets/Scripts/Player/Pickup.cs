using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public AudioSource pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
           // Destroy(this.gameObject);
            pickupSound.Play();

         //   if (this.gameObject.tag == "AmmoPickup")
    //        {
     //           other.GetComponent<PlayerShoot>().ReplenishAmmo();
      //      }

            if(this.gameObject.tag == "HealthPickup")
            {
                other.GetComponent<PlayerHealth>().ReplenishHealth();
                Debug.Log("Got some extra health!");
            }
        }
    }
}
