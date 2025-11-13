using UnityEngine;

[CreateAssetMenu(fileName = "OutGameUpgrades", menuName = "Scriptable Objects/OutGameUpgrades")]
public class OutGameUpgrades : ScriptableObject
{
	public float maxHealth;
	public float autoHealthRegeneration;
	public float xpGain;
	
	public float swordAndModulesUpgrade;
	public float playerSpeed;
	public float swordRadius;
	public float swordLength;
	public float swordDamages;
	public float swordSpeed;
	public float swordCooldownToDecrease;
}
