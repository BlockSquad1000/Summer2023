using UnityEngine;

using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform[] startPositions;

    //public int nextPlayersTeam;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if(PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(GameConstants.PLAYER_SELECTION, out object playerSelection))
            {
                if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue(GameMode.DeathMatch.ToString()))
                {
                    int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
                    PhotonNetwork.Instantiate(characterPrefabs[(int)playerSelection].name, startPositions[actorNumber - 1].position, Quaternion.identity);
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
