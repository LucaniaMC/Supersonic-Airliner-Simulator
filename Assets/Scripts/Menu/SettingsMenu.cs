using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider SFXslider;
    public Slider musicSlider;


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


    //I just like it
    public void ExitSettings()
    {
        AudioManager.instance.ApplyVolumeSettings();
    }


    //For handle buttons
    public void ClickSound()
    {
        AudioManager.instance.PlaySFX("Click", true);
    }
}
