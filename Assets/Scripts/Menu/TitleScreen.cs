using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject overlay;

    [SerializeField] TextMeshProUGUI versionNumberText;
    
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject levelMenu;
    [SerializeField] GameObject settingsMenu;


    void Start()
    {
        Invoke("DisableOverlay", 1f);
        optionsMenu.SetActive(false);
        levelMenu.SetActive(false);
        settingsMenu.SetActive(false);
        versionNumberText.text = "V " + Application.version;
        AudioManager.instance.PlayMusic("MenuMusic");
    }


    //turns off overlay to prevent it from blocking clicks
    void DisableOverlay()
    {
        overlay.SetActive(false);
    }


    #region Button Functions
    public void StartButton()
    {
        AudioManager.instance.PlaySFX("Click", true);
        levelMenu.SetActive(true);
    }


    public void ExitStart()
    {
        AudioManager.instance.PlaySFX("Click", true);
        levelMenu.SetActive(false);
    }


    public void OptionsButton()
    {
        AudioManager.instance.PlaySFX("Click", true);
        optionsMenu.SetActive(true);
    }


    public void ExitOptions()
    {
        AudioManager.instance.PlaySFX("Click", true);
        optionsMenu.SetActive(false);
    }


    public void SettingsButton()
    {
        AudioManager.instance.PlaySFX("Click", true);
        settingsMenu.SetActive(true);
    }


    public void ExitSettings()
    {
        AudioManager.instance.PlaySFX("Click", true);
        settingsMenu.SetActive(false);
    }
    #endregion


    #region Level Loading
    public void OpenLevel(string levelNumber)
    {
        AudioManager.instance.PlaySFX("Click", true);
        string levelName = "Level " + levelNumber;
        StartCoroutine(StartLevel(levelName));
    }


    public IEnumerator StartLevel(string levelName)
    {
        overlay.SetActive(true);
        animator.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
    }
    #endregion
}
