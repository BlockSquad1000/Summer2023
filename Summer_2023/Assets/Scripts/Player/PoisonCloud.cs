using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject poisonCloud;
    public bool poisionActive = false;

    public float activeCloudTime;
    public float cooldownTime;

    public AudioSource cloudAudio;

    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (!poisionActive && Input.GetKeyDown(KeyCode.R))
        {   
            StartCoroutine(PoisonCloudActivate());
            Debug.Log("Poison cloud is active.");        
        }
        if (poisionActive && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Poison cloud is already active, cannot be used at this time.");
        }
     
    }

    
    IEnumerator PoisonCloudActivate()
    {
        GameObject go = Instantiate(poisonCloud, spawnPoint.position, Quaternion.identity); //Spawns the cloud at the position behind the player.
        poisionActive = true; //While this bool is set to true, no more clouds can be spawned.
        cloudAudio.Play();
        yield return new WaitForSeconds(activeCloudTime); //This is how long the cloud is active for.
        anim = go.gameObject.GetComponent<Animator>();
        anim.Play("PoisonCloudShrink");
        yield return new WaitForSeconds(1.2f);
        Destroy(go.gameObject);
        Debug.Log("Poison cloud is inactive.");
        yield return new WaitForSeconds(cooldownTime); //This is how long the cooldown as until the cloud can be used again.
        poisionActive = false;
        Debug.Log("Cooldown time has ended, cloud can be used again.");
    }
}
