using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int totalHealth;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth += totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }
}
