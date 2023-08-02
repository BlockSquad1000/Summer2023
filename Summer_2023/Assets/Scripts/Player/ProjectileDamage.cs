using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            other.GetComponent<Health>().Damage(5);
            Debug.Log("Hit asteroid.");
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().ApplyDamage(5);
            Debug.Log("Hit player.");
            Destroy(this.gameObject);
        }
    }
}
