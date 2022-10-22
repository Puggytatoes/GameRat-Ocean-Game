using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 
public class OptionsMenu : MonoBehaviour
{
    //Quick and simple options menu to control volume. 
    [SerializeField] private AudioMixer audioMixer;

    public void MuteToggle(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0; 
        }
        else
        {
            AudioListener.volume = 1; 
        }
    }
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("bgmVol", volume); 
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVol", volume); 
    }

}
