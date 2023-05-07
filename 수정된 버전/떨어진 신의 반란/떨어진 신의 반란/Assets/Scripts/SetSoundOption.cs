using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class SetSoundOption : MonoBehaviour
{
    public AudioMixer audioMixer;

    void Start()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(DataManager.instance.database.playerData.audioData.bgm) * 20);
        audioMixer.SetFloat("Master", Mathf.Log10(DataManager.instance.database.playerData.audioData.master) * 20);
    }
    void Update()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(DataManager.instance.database.playerData.audioData.bgm) * 20);
        audioMixer.SetFloat("Master", Mathf.Log10(DataManager.instance.database.playerData.audioData.master) * 20);
    }
}
