using UnityEngine;

public class XpGain : MonoBehaviour
{
    public Bottle1ScriptableObject bottleData;
    public AudioSource             recuperation;

    public void OnTriggerEnter2D(Collider2D cl2D)
    {
        if (cl2D.gameObject.tag == "Player")
        {
            PlayerStats.SINGLETON.IncreaseExperience(bottleData.xpGift);

            // Détache l'audio de l'objet
            recuperation.transform.parent = null;

            // Joue le son
            recuperation.Play();

            Destroy(gameObject);

            // Détruit l'AudioSource une fois terminé
            Destroy(recuperation.gameObject, recuperation.clip.length);
            
        }
    }
}

