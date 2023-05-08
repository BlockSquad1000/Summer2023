using System;
using UnityEngine;

[System.Serializable]
public struct GameSettings
{
    [Header("Global Settings")]
    public byte maxPlayers;
    public GameMode gameMode;
}
