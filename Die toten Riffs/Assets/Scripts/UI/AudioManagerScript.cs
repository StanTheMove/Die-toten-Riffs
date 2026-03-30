using System.Collections;
using UnityEngine;
public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript instance;

    private void Awake()
    {
        instance = this;
        SetVolume(HolderScript.Volume);
    }

    public void SetVolume(float volume)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    public void PlaySFX(AudioClip audioClip, float volume = 1f)
    {
        StartCoroutine(PlaySFXCoroutine(audioClip, volume));
    }

    IEnumerator PlaySFXCoroutine(AudioClip audioClip, float volume = 1f)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length * 2);
        Destroy(audioSource);
    }
}