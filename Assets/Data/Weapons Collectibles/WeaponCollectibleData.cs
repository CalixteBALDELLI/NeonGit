using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WeaponCollectibleData", menuName= "ScriptableObjects/WeaponCollectible")]
public class WeaponCollectibleData : ScriptableObject
{
    public string weaponName;
    public int    weaponId;
    public int    xpValue;
    public string description;
    public Sprite  weaponIcon;

}
