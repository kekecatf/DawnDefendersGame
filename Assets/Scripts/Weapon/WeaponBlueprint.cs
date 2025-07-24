using UnityEngine;
using System.Collections.Generic;

// Bu, bir silah� craftlamak i�in gereken par�alar� ve miktarlar�n� tan�mlar.
[System.Serializable]
public class PartRequirement
{
    public WeaponPartType partType;
    public int amount;
}

[CreateAssetMenu(fileName = "New Weapon Blueprint", menuName = "Crafting/Weapon Blueprint")]
public class WeaponBlueprint : ScriptableObject
{
    public string weaponName;
    public Sprite weaponIcon;

    // Craft edilecek silah�n WeaponSlotManager'daki slot index'i
    // 0: Makineli, 1: Tabanca, 2: K�l�� ...
    public int weaponSlotIndexToUnlock;

    // Bu tarif i�in gereken par�alar�n listesi
    public List<PartRequirement> requiredParts;
}