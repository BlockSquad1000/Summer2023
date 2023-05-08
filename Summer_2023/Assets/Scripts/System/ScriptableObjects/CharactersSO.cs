using UnityEngine;

[CreateAssetMenu(fileName = "Characters", menuName = "Player Mercenaries")]
public class CharactersSO : ScriptableObject
{
    [Header("Chaarcter Properties")]
    public string characterName;
    public Sprite characterSprite;
}
