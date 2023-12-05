using UnityEngine;

public enum PlayerTeam
{
    RedTeam = 1,
    BlueTeam = 2
}

[CreateAssetMenu(fileName = "Teams", menuName = "Player Teams")]
public class PlayerTeamSO : ScriptableObject
{
    public PlayerTeam team;
}
