using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
   
   [SerializeField] private Slider musicSlider = null;
   [SerializeField] private Slider sfxSlider = null;

   [SerializeField] private TextMeshProUGUI musicTextUI = null;
   [SerializeField] private TextMeshProUGUI sfxTextUI = null;

   [SerializeField] private AudioMixer audioMixer;
 
 
   private void Start()
   {
       LoadValues();
   }
 
  
   public void MusicSlider(float volume)
   {
       musicTextUI.text = volume.ToString("0.0");
   }
 
 
   public void SetMusicVolume(float sliderValue)
   {
       audioMixer.SetFloat("GameMusic", Mathf.Log10(sliderValue) * 20);
   }

   public float GetMusicVolume()
   {
       float value = 0;
       audioMixer.GetFloat("GameMusic", out value);
       return value;
   }

   public void SFXSlider(float volume)
    {
        sfxTextUI.text = volume.ToString("0.0");
    }


    public void SetSFXVolume(float sliderValue)
    {
        audioMixer.SetFloat("GameSFX", Mathf.Log10(sliderValue) * 20);
    }



    public void SaveVolumeButton()
   {
       float musicValue = musicSlider.value;
        float sfxValue = sfxSlider.value;
       
       PlayerPrefs.SetFloat("MusicValue", musicValue);
        PlayerPrefs.SetFloat("SFXValue", sfxValue);
 
       LoadValues();
   }
 
   public void LoadValues()
   {

        float musicValue = PlayerPrefs.GetFloat("MusicValue", 1);
        float sfxValue = PlayerPrefs.GetFloat("SFXValue", 1);

       musicSlider.value = musicValue;
        sfxSlider.value = sfxValue;

       SetMusicVolume(musicValue);
        SetSFXVolume(sfxValue);
   }

}
