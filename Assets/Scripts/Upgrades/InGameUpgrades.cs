using UnityEngine;

[CreateAssetMenu(fileName = "InGameUpgrades", menuName = "ScriptableObjects/InGameUpgrades")]
public class InGameUpgrades : ScriptableObject
{
    public float playerDamages;
    public float playerSpeed;
    public float swordRadius;
    public float swordLength;
    public float swordSpeed;
    public float swordCooldownToDecrease;

}
