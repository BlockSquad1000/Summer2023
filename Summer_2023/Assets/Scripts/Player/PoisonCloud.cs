using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    public GameObject poisonCloud;
    public bool poisionActive = false;

    public AudioSource cloudAudio;

    // Start is called before the first frame update
    void Start()
    {
        poisonCloud.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
     if (Input.GetKeyDown(KeyCode.R) && !poisionActive)
        {
            StartCoroutine(ResetPoisonCloud());
            Debug.Log("Poison cloud is active.");
        }
    }

    IEnumerator ResetPoisonCloud()
    {
        poisonCloud.SetActive(true);
        poisionActive = true;
        cloudAudio.Play();
        yield return new WaitForSeconds(3);
        poisonCloud.SetActive(false);
        poisionActive = false;
        Debug.Log("Poison cloud is inactive.");
    }
}
