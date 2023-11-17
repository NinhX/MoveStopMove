using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private Sound[] sounds;

    private static AudioSource audioSource;

    private Dictionary<SoundType, AudioClip> dictClip = new();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat(Constant.VOLUME_PLAYER_PREF, 1);
        dictClip.Clear();
        foreach (var sound in sounds)
        {
            dictClip.Add(sound.Type, sound.AudioClip);
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SaveVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(Constant.VOLUME_PLAYER_PREF, audioSource.volume);
    }

    public void PlaySound(SoundType type)
    {
        if (dictClip.ContainsKey(type))
        {
            AudioClip sound = dictClip[type];
            PlaySound(sound);
        }
    }

    public void PlayMusic(SoundType type)
    {
        if (dictClip.ContainsKey(type))
        {
            AudioClip sound = dictClip[type];
            PlayMusic(sound);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip || !audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}