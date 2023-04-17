using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class SoundOption : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider masterSlider;
    public Slider bgmSlider;

    public void SetMasterVolume()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(masterSlider.value) * 20);
    }

    public void SetBGMVolume()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
    }
}
