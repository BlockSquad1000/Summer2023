using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

public class PlayerHealth : MonoBehaviourPun
{
    public float totalHealth;
    public Image healthBar;
    public float currentHealth;
    public int killValue;

    public GameObject playerUIObject;
    public int respawnTime = 10;

    private GameObject deathUIPanel;
    private Text respawnTimerUIText;

    // Start is called before the first frame update
    void Start()
    {
        if(killValue == 0)
        {
            killValue = 1;
        }

        currentHealth += totalHealth;
        healthBar.fillAmount = 1f;

        if (photonView.IsMine)
        {
            deathUIPanel = GameObject.Find("DeathScreen");
            respawnTimerUIText = deathUIPanel.transform.Find("TXT_Countdown").GetComponent<Text>();

            deathUIPanel.SetActive(false);
        }
    }

    [PunRPC]
    public void ApplyDamage(float damage)
    {
        if (currentHealth <= 0f) return;

        currentHealth -= damage;

        healthBar.fillAmount = currentHealth / totalHealth;

        if(currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        ChangeLivingState(false);

        if (photonView.IsMine)
        {
            respawnTimerUIText.text = respawnTime.ToString();
            StartCoroutine(Respawn(respawnTime));
        }
    }

    private void ChangeLivingState(bool alive)
    {
        //weaponObject.SetActive(alive);
        playerUIObject.SetActive(alive);

        if (photonView.IsMine)
        {
            deathUIPanel.SetActive(!alive);
        }
    }

    private IEnumerator Respawn(int timeToRespawn)
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerShoot>().enabled = false;

        while (timeToRespawn > 0)
        {
            yield return new WaitForSeconds(1f);
            timeToRespawn--;

            respawnTimerUIText.text = timeToRespawn.ToString();
        }

        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerShoot>().enabled = true;

        float randomPos = Random.Range(-35, 36);
        transform.position = new Vector3(randomPos, 0, randomPos);

        photonView.RPC("RestorePlayer", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RestorePlayer()
    {
        currentHealth = totalHealth;
        healthBar.fillAmount = 1f;

        ChangeLivingState(true);
    }
}
