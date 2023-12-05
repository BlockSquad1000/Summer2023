using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ExitGames.Client.Photon;
using Photon.Pun;

public class PlayerTeamSelection : MonoBehaviour
{
    public static PlayerTeamSelection Instance { get; private set; }
    public bool inTeamOne = false;
    public bool inTeamTwo = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void JoinTeam(int thisTeam)
    {
        if (thisTeam == 1)
        {
            inTeamOne = true;
            inTeamTwo = false;
        }
        if (thisTeam == 2)
        {
            inTeamOne = false;
            inTeamTwo = true;
        }
    }
}
