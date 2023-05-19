using System;
using UnityEngine;

using ExitGames.Client.Photon;
using Photon.Pun;

public class PlayerSelection : MonoBehaviour
{
    public GameObject[] selectableCharacters;

    private int activeCharacter = 0;
    public int ActiveCharacter
    {
        get { return activeCharacter; }
        set
        {
            activeCharacter = Mathf.Clamp(value, 0, selectableCharacters.Length - 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ActivateCharacter(activeCharacter);
    }

    private void ActivateCharacter(int thisPlayer)
    {
        foreach(GameObject character in selectableCharacters)
        {
            character.SetActive(false);
        }

        selectableCharacters[thisPlayer].SetActive(true);

        Hashtable playerSelectionProp = new() { { GameConstants.PLAYER_SELECTION, thisPlayer } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerSelectionProp);
    }

    public void ChangeActiveCharacter(int direction)
    {
        activeCharacter += direction;
        if(activeCharacter >= selectableCharacters.Length)
        {
            activeCharacter = 0;
        }
        else if (activeCharacter == -1)
        {
            activeCharacter = selectableCharacters.Length - 1;
        }
        ActivateCharacter(activeCharacter);
    }
 
}
