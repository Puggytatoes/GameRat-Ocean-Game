//Contributors: Eric Park, Kenneth Tang
using UnityEngine.Audio; 
using UnityEngine;

[System.Serializable]
public class Sound
{
    //Simple containter class for audio. 
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;
    public bool loop;
    [HideInInspector] public AudioSource source;
    public AudioMixerGroup mixerGroup; 
}
