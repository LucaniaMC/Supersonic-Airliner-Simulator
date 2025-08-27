using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelMenu : UIMenu
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject overlay;


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
}
