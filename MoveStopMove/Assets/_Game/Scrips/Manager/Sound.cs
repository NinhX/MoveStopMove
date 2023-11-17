using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private SoundType type;
    [SerializeField] private AudioClip audioClip;

    public SoundType Type => type;
    public AudioClip AudioClip => audioClip;
}
