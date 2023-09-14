using System.Collections;
using UnityEngine;

using Photon.Pun;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(PlayerInitializer))]
public class Countdown : MonoBehaviourPunCallbacks
{
    private byte countdownTimer;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            countdownTimer = GameManager.Instance.settings.timeToGameStart;
            photonView.RPC("SetTime", RpcTarget.AllBuffered, countdownTimer);
            StartCoroutine(CountdownTick());
        }
    }

    [PunRPC]
    public void SetTime(byte time)
    {
        if(time > 0)
        {
            DeathmatchUIManager.Instance.SetCountdownUIText(time.ToString());
            GetComponent<PlayerMovement>().DisableControls();
            GetComponent<PlayerShoot>().enabled = false;
        }
        else
        {
            DeathmatchUIManager.Instance.SetCountdownUIText("BEGIN!");
            GetComponent<PlayerMovement>().EnableControls();
            GetComponent<PlayerShoot>().enabled = true;
            StartCoroutine(ClearCountdownText());
        }
    }

    private IEnumerator CountdownTick()
    {
        while(countdownTimer > 0)
        {
            countdownTimer--;
            yield return new WaitForSeconds(1);
            photonView.RPC("SetTime", RpcTarget.AllBuffered, countdownTimer);
        }
    }

    private IEnumerator ClearCountdownText()
    {
        yield return new WaitForSeconds(5);
        DeathmatchUIManager.Instance.SetCountdownUIText(string.Empty);
        enabled = false;
    }
}
