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

    void Start()
    {
        masterSlider.value = DataManager.instance.database.playerData.audioData.master;
        bgmSlider.value = DataManager.instance.database.playerData.audioData.bgm;
    }

    public void SetMasterVolume()
    {
        DataManager.instance.database.playerData.audioData.master = masterSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(masterSlider.value) * 20);
        DataManager.instance.JsonSave();
    }

    public void SetBGMVolume()
    {
        DataManager.instance.database.playerData.audioData.bgm = bgmSlider.value;
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
        DataManager.instance.JsonSave();
    }
}
