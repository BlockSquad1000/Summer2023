using UnityEngine;

[CreateAssetMenu(fileName = "Characters", menuName = "Player Mercenaries")]
public class CharactersSO : ScriptableObject
{
    [Header("Chaarcter Properties")]
    public string characterName;
    public Sprite characterSprite;

    [Header("Weapon Properties")]
    public string weaponName;
    public float weaponDamage;
    public float rateOfFire;
    public float projectileSpeed;
    public int ammoCapacity;
}
