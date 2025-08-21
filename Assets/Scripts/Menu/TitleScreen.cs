using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject overlay;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject levelMenu;
    [SerializeField] GameObject settingsMenu;

    private string levelName;


    void Start()
    {
        Invoke("DisableOverlay", 1f);
        optionsMenu.SetActive(false);
        levelMenu.SetActive(false);
        AudioManager.instance.PlayMusic("MenuMusic");
    }

    void DisableOverlay() 
    {
        overlay.SetActive(false);
    }

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

    public void OpenLevel(string levelNumber)
    {
        AudioManager.instance.PlaySFX("Click", true);
        levelName = "Level " + levelNumber;
        StartCoroutine(StartLevel());
    }

    public IEnumerator StartLevel()
    {
        overlay.SetActive(true);
        animator.SetBool("FadeIn",true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
    }

}
