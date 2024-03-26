using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Controllers.Menu{
    public class SettingsMenuController : MonoBehaviour
    {
        public AudioMixer AudioMixer;
        public void SetMasterVolume(float volume){
            AudioMixer.SetFloat("masterVolume", volume);
        }

        public void SetMusicVolume(float volume){
            AudioMixer.SetFloat("musicVolume", volume);
        }
    }
}