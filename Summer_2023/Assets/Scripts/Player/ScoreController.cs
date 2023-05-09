using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using ExitGames.Client.Photon;
using TMPro;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(PlayerInitializer))]
public class ScoreController : MonoBehaviourPun
{
    [Header("Score Variables")]
    public int totalScore;
    public int currentScore;

    public AudioSource winMusic;

    public bool won;
    public enum EventCode
    {
        WhoWon = 0
    }

    private byte scoreOrder = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(GameConstants.SCORE))
        {
            totalScore = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameConstants.SCORE];
        }
        else
        {
            totalScore = 15;
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEventReceived;
    }

    private void OnEventReceived(EventData eventData)
    {
        if(eventData.Code == (byte)EventCode.WhoWon)
        {
            object[] data = eventData.CustomData as object[];

            if(data != null)
            {
                string nickname = (string)data[0];
                scoreOrder = (byte)data[1];
                int viewID = (int)data[2];

                GameObject scoreOrderUIObject = DeathmatchUIManager.Instance.GetScoreOrderUIObject(scoreOrder - 1);
                scoreOrderUIObject.SetActive(true);

                if(viewID == photonView.ViewID)
                {
                    
                }
                scoreOrderUIObject.GetComponent<TMP_Text>().text = $"{scoreOrder}. {nickname}";

                Debug.Log($"{nickname} came in {scoreOrder} place.");
            }
        }
    }

    private void ScoreIncrease()
    {
        if (currentScore >= totalScore)
        {
            GameFinished();
        }
    }

    private void GameFinished()
    {
        scoreOrder += 1;

        GetComponent<PlayerInitializer>().playerCam.transform.parent = null;
        GetComponent<PlayerMovement>().enabled = false;

        DeathmatchUIManager.Instance.FinishUIBackground.SetActive(true);
        winMusic.Play();
        DeathmatchUIManager.Instance.finished = true;

        int viewID = photonView.ViewID;

        object[] data = new object[] { photonView.Owner.NickName, scoreOrder, viewID };

        RaiseEventOptions eventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache
        };

        SendOptions sendOptions = new SendOptions
        {
            Reliability = false
        };

        PhotonNetwork.RaiseEvent((byte)EventCode.WhoWon, data, eventOptions, sendOptions);
    }
}
