using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject overlay;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject levelMenu;

    private string levelName;

    private AudioManager audioManager;


    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        Invoke("DisableOverlay", 1f);
        optionsMenu.SetActive(false);
        levelMenu.SetActive(false);
    }

    void DisableOverlay() 
    {
        overlay.SetActive(false);
    }

    public void StartButton() 
    {
        audioManager.PlaySFX("Click", true);
        levelMenu.SetActive(true);
    }

    public void ExitStart() 
    {
        audioManager.PlaySFX("Click", true);
        levelMenu.SetActive(false);
    }

    public void OptionsButton() 
    {
        audioManager.PlaySFX("Click", true);
        optionsMenu.SetActive(true);
    }

    public void ExitOptions() 
    {
        audioManager.PlaySFX("Click", true);
        optionsMenu.SetActive(false);
    }

    public void OpenLevel (int levelID) 
    {
        audioManager.PlaySFX("Click", true);
        levelName = "Level " + levelID;
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
