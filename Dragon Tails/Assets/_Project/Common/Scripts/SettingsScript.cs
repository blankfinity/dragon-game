using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsScript : MonoBehaviour {

    public float volume;
    public int GraphicsQualityIndex;
    public AudioMixer audioMixer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetVolume()
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetGraphicsQuality(int QualityIndex)
    {
        this.GraphicsQualityIndex = QualityIndex;
        QualitySettings.SetQualityLevel(GraphicsQualityIndex);
    }
}
