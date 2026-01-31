using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource audioSource, textBlipSound;

    void Awake()
    {
        if (AudioManager.instance != null) Destroy(gameObject);
        else AudioManager.instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound()
    {

    }

    public void PlayBlipTextSound()
    {
        textBlipSound.pitch = Random.Range(-1f, 1f);
        textBlipSound.Play();
    }
}
