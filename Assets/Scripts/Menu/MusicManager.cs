using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public        AudioClip    musicbackground; // La musique de fond 
	public        AudioSource  musicSource;     
	public static MusicManager instance;        

	private void Awake()
	{
		
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);  
		}
		else
		{
			// Si l'instance existe déjà, vérifie si la musique est la même
			if (instance.musicSource.clip != musicbackground)
			{
				// Si la musique est différente, remplace la musique
				instance.musicSource.clip = musicbackground;
				instance.musicSource.Play();
			}
			Destroy(gameObject); 
		}
	}

	private void Start()
	{
		
		if (!musicSource.isPlaying)
		{
			musicSource.clip = musicbackground;
			musicSource.Play();
		}
	}

	private void Update()
	{
		DontDestroyOnLoad(gameObject);
	}
}