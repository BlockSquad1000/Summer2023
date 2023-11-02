using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWeaponDamage : MonoBehaviour
{
    //public CharactersSO playerProperties;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().slowEffectActive = true;
            other.GetComponent<PlayerMovement>().SlowEffect();
        }
    }

    private void OnCollisionExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().slowEffectActive = false;
            other.GetComponent<PlayerMovement>().SlowEffect();
        }
    }
}
