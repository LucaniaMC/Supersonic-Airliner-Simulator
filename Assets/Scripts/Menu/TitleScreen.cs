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

    void Start()
    {
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
        FindObjectOfType<AudioManager>().PlaySFX("Click", true);
        levelMenu.SetActive(true);
    }

    public void ExitStart() 
    {
        FindObjectOfType<AudioManager>().PlaySFX("Click", true);
        levelMenu.SetActive(false);
    }

    public void OptionsButton() 
    {
        FindObjectOfType<AudioManager>().PlaySFX("Click", true);
        optionsMenu.SetActive(true);
    }

    public void ExitOptions() 
    {
        FindObjectOfType<AudioManager>().PlaySFX("Click", true);
        optionsMenu.SetActive(false);
    }

    public void OpenLevel (int levelID) 
    {
        FindObjectOfType<AudioManager>().PlaySFX("Click", true);
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
