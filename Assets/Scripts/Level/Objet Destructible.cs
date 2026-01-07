using UnityEngine;

public class DestructibleFurniture : MonoBehaviour
{
	public                 string      destructTag = "PlayerSword";
	public                 Sprite      destroyedSprite;
	public                 AudioClip[] destructionSounds;
	[Range(0f, 1f)] public float       volume = 1f;

	private AudioSource    audioSource;
	private SpriteRenderer spriteRenderer;
	private bool           isDestroyed = false;
	public  int            MoneyToAdd  = 10;
	PlayerStats playerStats;
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		audioSource    = GetComponent<AudioSource>();
		if (audioSource  == null) audioSource          = gameObject.AddComponent<AudioSource>();
		playerStats   = FindAnyObjectByType<PlayerStats>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (isDestroyed) return;
		if (other.CompareTag(destructTag))
		{
			isDestroyed = true;
			if (destroyedSprite != null) spriteRenderer.sprite = destroyedSprite;
			if (destructionSounds.Length > 0)
			{
				AudioClip clip = destructionSounds[Random.Range(0, destructionSounds.Length)];
				audioSource.PlayOneShot(clip, volume);
				playerStats.AddMoney(MoneyToAdd);
					
			}
		}
	}
}