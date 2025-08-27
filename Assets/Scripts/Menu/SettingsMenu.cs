using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : UIMenu
{
    public Slider SFXslider;
    public Slider musicSlider;


    //Set slide values
    public void Initialize()
    {
        SFXslider.value = AudioManager.instance.SFXVolume;
        musicSlider.value = AudioManager.instance.musicVolume;
    }


    //Called in SFX slider button event
    public void OnSFXValueChange()
    {
        AudioManager.instance.SetSFXVolume(SFXslider.value);
    }


    //Called in music slider button event
    public void OnMusicValueChange()
    {
        AudioManager.instance.SetMusicVolume(musicSlider.value);
    }


    //Called in back button event
    public void ExitSettings()
    {
        AudioManager.instance.ApplyVolumeSettings();
    }
}
