using System;
using UnityEngine;

public static class UtilSoundManager
{

    public static void PlaySoundCoin(GameObject parent){
        PlaySound(parent, UtilEnum.Sounds.Coin, 1);
    }

      public static void PlaySoundHit(GameObject parent){
        PlaySound(parent, UtilEnum.Sounds.Hit, 1);
    }

      public static void PlaySoundHealing(GameObject parent){
        PlaySound(parent, UtilEnum.Sounds.Healing, 2);
    }

      public static void PlaySoundSpikeTrap(GameObject parent){
        PlaySound(parent, UtilEnum.Sounds.SpikeTrap, 1);
    }

    public static void PlaySound(GameObject parent, UtilEnum.Sounds name, int destroyTime)
    {
        GameObject soundObject = new GameObject("sound_"+name.ToString());
        soundObject.transform.position = parent.transform.position;
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + name.ToString());
        audioSource.clip = clip;
        audioSource.Play();
        GameObject.Destroy(soundObject, destroyTime);
    }
}
