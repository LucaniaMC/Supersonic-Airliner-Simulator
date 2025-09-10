using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelMenu : UIMenu
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject overlay;

    [SerializeField] private int currentPage = 0;
    [SerializeField] private GameObject[] pages;    //reference for all page objects


    public void NextPage()
    { 
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
    }


    public void PreviousPage()
    { 
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
        }
    }


    void ShowPage(int index)
    { 
        // Hide all pages first
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }
    }


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
