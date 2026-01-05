using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
	public  AudioSource audioSource;            // Référence à l'AudioSource
	public  AudioClip[] soundEffects;           // Liste des bruitages à choisir
	private bool        isSoundPlaying = false; // Indique si un bruitage est déjà en cours

	// Méthode qui est appelée lorsque quelque chose entre dans le trigger 2D
	private void OnTriggerEnter2D(Collider2D other)
	{
		// Si l'objet détecté est un joueur ou un ennemi
		if ((other.CompareTag("Player") || other.CompareTag("Enemy")) && !isSoundPlaying)
		{
			// Choisir un bruitage aléatoire parmi la liste
			AudioClip randomClip = soundEffects[Random.Range(0, soundEffects.Length)];

			// Vérifie si l'AudioSource est prêt à jouer un nouveau son
			if (audioSource != null && !audioSource.isPlaying)
			{
				// Met l'indicateur à true pour éviter de rejouer un son tant qu'il est en cours
				isSoundPlaying = true;

				// Assigne le bruitage aléatoire à l'AudioSource et joue le son
				audioSource.clip = randomClip;
				audioSource.Play();

				Debug.Log("Son joué : " + randomClip.name);  // Affiche quel son a été joué
			}
			else
			{
				Debug.LogError("AudioSource est null ou déjà en train de jouer un son !");
			}
		}
	}

	// Méthode pour vérifier si le son est terminé
	private void Update()
	{
		// Si l'AudioSource n'est plus en train de jouer un son, on peut en jouer un nouveau
		if (!audioSource.isPlaying)
		{
			isSoundPlaying = false;
		}
	}
}