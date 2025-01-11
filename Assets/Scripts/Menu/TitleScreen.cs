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
        FindObjectOfType<AudioManager>().Play("Click");
        levelMenu.SetActive(true);
    }

    public void ExitStart() 
    {
        FindObjectOfType<AudioManager>().Play("Click");
        levelMenu.SetActive(false);
    }

    public void OptionsButton() 
    {
        FindObjectOfType<AudioManager>().Play("Click");
        optionsMenu.SetActive(true);
    }

    public void ExitOptions() 
    {
        FindObjectOfType<AudioManager>().Play("Click");
        optionsMenu.SetActive(false);
    }

    public void OpenLevel (int levelID) 
    {
        FindObjectOfType<AudioManager>().Play("Click");
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
