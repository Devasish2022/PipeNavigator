using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;

    public static AudioManager Instance = null;

    private GameOverManager gameOverManager;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(Instance);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void Play(AudioClip clip)
    {
        effectSource.clip = clip;
        effectSource.Play();
    }

    public void PlayAndThenReload(AudioClip clip)
    {
        effectSource.clip = clip;
        effectSource.Play();
        StartCoroutine(ReloadAfterPlayer(clip.length));
    }

    private IEnumerator ReloadAfterPlayer(float delay)
    {
        musicSource.Pause();
        yield return new WaitForSeconds(delay);
        gameOverManager = FindAnyObjectByType<GameOverManager>();
        gameOverManager.LoadTheMenu();
        musicSource.Play();
    }
}
