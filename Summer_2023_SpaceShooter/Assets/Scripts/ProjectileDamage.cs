using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            other.GetComponent<AsteroidHealth>().DamageAsteroid(5);
            Debug.Log("Hit asteroid.");
            Destroy(this.gameObject);
        }
    }
}
