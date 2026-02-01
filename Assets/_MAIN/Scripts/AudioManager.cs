using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sFXSource;

    public AudioClip choiceSFX;
    public AudioClip breakSFX;
    public AudioClip fullBreakSFX;


    public void PlaySFX(AudioClip audioClip)
    {
        sFXSource.PlayOneShot(audioClip);
    }



}
