using UnityEngine;

using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform[] startPositions;

    public GameObject playerTeam;

    // Start is called before the first frame update
    void Start()
    {
        playerTeam = GameObject.FindWithTag("PlayerTeamSelect");

        if (PhotonNetwork.IsConnectedAndReady)
        {
            if(PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(GameConstants.PLAYER_SELECTION, out object playerSelection))
            {
                if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue(GameMode.DeathMatch.ToString()))
                {
                    int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
                    var go = PhotonNetwork.Instantiate(characterPrefabs[(int)playerSelection].name, startPositions[actorNumber - 1].position, Quaternion.identity);

                    if(playerTeam.GetComponent<PlayerTeamSelection>().inTeamOne == true)
                    {
                        GameObject[] teamOnePlayers = GameObject.FindGameObjectsWithTag("TeamOne");
                        int totalTeamOnePlayers = teamOnePlayers.Length;

                        go.gameObject.tag = "TeamOne";
                        if(totalTeamOnePlayers >= 6)
                        {
                            go.gameObject.tag = "TeamTwo";
                            Debug.Log("Team One is full. Joining Team Two.");
                        }
                    }
                    if(playerTeam.GetComponent<PlayerTeamSelection>().inTeamTwo == true)
                    {
                        GameObject[] teamTwoPlayers = GameObject.FindGameObjectsWithTag("TeamTwo");
                        int totalTeamTwoPlayers = teamTwoPlayers.Length;

                        go.gameObject.tag = "TeamTwo";
                        if (totalTeamTwoPlayers >= 6)
                        {
                            go.gameObject.tag = "TeamOne";
                            Debug.Log("Team Two is full. Joining Team One.");
                        }
                    }
                }
                else if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue(GameMode.CaptureTheFlag.ToString()))
                {
                    int randomPosition = Random.Range(-35, 35);
                    PhotonNetwork.Instantiate(characterPrefabs[(int)playerSelection].name, new Vector3(randomPosition, 0f, randomPosition), Quaternion.identity);
                }
            }          
        }
    }
}
