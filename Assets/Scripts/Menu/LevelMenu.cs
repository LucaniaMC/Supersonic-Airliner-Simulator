using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelMenu : UIMenu
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject overlay;

    [SerializeField] private int currentPage = 0;
    [SerializeField] private GameObject[] pages;    //reference for all page objects


    void Start()
    {
        ShowPage(currentPage);    
    }


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
        // Go through each page and set the current one active
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }
    }


    public void OpenLevel(string levelNumber)
    {
        string levelName = "Level " + levelNumber;
        StartCoroutine(StartLevel(levelName));
    }


    IEnumerator StartLevel(string levelName)
    {
        overlay.SetActive(true);
        animator.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
    }
}
