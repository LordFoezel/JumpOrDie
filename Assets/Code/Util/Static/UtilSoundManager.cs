using System;
using UnityEngine;

public static class UtilSoundManager
{
    public static void PlaySound(GameObject parent, UtilEnum.Sounds name = UtilEnum.Sounds.NoSound)
    {
        AudioSource audioSource = parent.GetComponent<AudioSource>();
        if (name != UtilEnum.Sounds.NoSound)
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/" + name.ToString());
            audioSource.clip = clip;
        }
        audioSource.Play();
    }

    public static AudioSource AddSound(GameObject parent, UtilEnum.Sounds name)
    {
        AudioSource audioSource = parent.AddComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + name.ToString());
        audioSource.clip = clip;
        audioSource.playOnAwake = false;
        return audioSource;
    }
}
