using UnityEngine;

using Photon.Pun;

using TMPro;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInitializer : MonoBehaviourPunCallbacks
{
    public Camera playerCam;
    [SerializeField] private TMP_Text playerName;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue(GameMode.DeathMatch.ToString()))
        {
            ScoreController scoreController = GetComponent<ScoreController>();
            if (photonView.IsMine)
            {
                if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(GameConstants.SCORE, out object score))
                {
                    scoreController.totalScore = (int)score;
                }
            }
            else
            {
                GetComponent<ScoreController>().enabled = false;
            }
        }

        if (photonView.IsMine)
        {
            GetComponent<PlayerMovement>().enabled = true;
            playerCam.gameObject.SetActive(true);
        }
        else
        {
            GetComponent<PlayerMovement>().enabled = false;
            playerCam.gameObject.SetActive(false);
        }

        playerName.text = photonView.Owner.NickName;
        if (photonView.IsMine)
        {
            playerName.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
