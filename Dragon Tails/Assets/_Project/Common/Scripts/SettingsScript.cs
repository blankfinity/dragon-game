using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {

    public float volume;
    public int GraphicsQualityIndex;
    public AudioMixer audioMixer;
    public Dropdown QualitySettingDropdown;
	// Use this for initialization
	void Start () {
        GraphicsQualityIndex = QualitySettings.GetQualityLevel();
        QualitySettingDropdown.value = GraphicsQualityIndex;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetVolume(float Volume)
    {
        this.volume = Volume;
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetGraphicsQuality(int QualityIndex)
    {
        this.GraphicsQualityIndex = QualityIndex;
        QualitySettings.SetQualityLevel(GraphicsQualityIndex);
    }
}
