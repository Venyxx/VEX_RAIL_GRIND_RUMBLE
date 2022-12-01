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
 
   [SerializeField] private TextMeshProUGUI musicTextUI = null;
 
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
 
     
 
   public void SaveVolumeButton()
   {
       float musicValue = musicSlider.value; 
       
       PlayerPrefs.SetFloat("MusicValue", musicValue);
 
       LoadValues();
   }
 
   public void LoadValues()
   {

        float musicValue = PlayerPrefs.GetFloat("MusicValue");

       musicSlider.value = musicValue;

       SetMusicVolume(musicValue);
   }

}
