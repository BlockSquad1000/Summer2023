using UnityEngine;

public enum GameMode
{
    DeathMatch = 1,
    CaptureTheFlag = 2
}

[CreateAssetMenu(fileName = "Mode_", menuName = "Game Mode", order = 1)]
public class GameModeSO : ScriptableObject
{
    public GameMode mode;
}
