using UnityEngine;





public class MusicManager : MonoBehaviour
{
	public AudioClip musicbackground;
	public AudioSource musicSource;
	
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
			Destroy(gameObject);
		}
	}
	
	
	
	
	private void Start()
	{
		musicSource.clip = musicbackground;
		musicSource.Play();
	}
	
	
	private void Update()
	{
		DontDestroyOnLoad(gameObject);
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
