using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenuController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void setMasterVolume(float volume){
        audioMixer.SetFloat("masterVolume", volume);
    }
}
