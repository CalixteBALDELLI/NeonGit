using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireController2D : MonoBehaviour
{
	[Header("Light Settings")]
	public Light2D fireLight;         // Ta lumière 2D
	public float minIntensity = 0.8f; // Valeur minimum
	public float maxIntensity = 1.2f; // Valeur maximum
	public float flickerSpeed = 5f;   // Vitesse du scintillement

	[Header("Audio Settings")]
	public AudioSource fireSound;         // Son de feu

	private void Start()
	{
		// Démarrage du son
		if (fireSound != null)
		{
			fireSound.loop = true;
			fireSound.Play();
		}
	}

	private void Update()
	{
		if (fireLight != null)
		{
			// Variation aléatoire douce entre min/max
			float targetIntensity = Random.Range(minIntensity, maxIntensity);
			fireLight.intensity = Mathf.Lerp(
				fireLight.intensity,
				targetIntensity,
				Time.deltaTime * flickerSpeed
			);
		}
	}
}