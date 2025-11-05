using UnityEngine;

public class DestructibleFurniture : MonoBehaviour
{
	public                 string      destructTag;
	public                 Sprite      intactSprite;
	public                 Sprite      destroyedSprite;
	public                 AudioClip[] destructionSounds;
	[Range(0f, 1f)] public float       volume = 1f;

	private AudioSource    audioSource;
	private SpriteRenderer spriteRenderer;
	private bool           isDestroyed = false;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		audioSource    = GetComponent<AudioSource>();
		if (audioSource  == null) audioSource           = gameObject.AddComponent<AudioSource>();
		if (intactSprite != null) spriteRenderer.sprite = intactSprite;
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
			}
		}
	}
}