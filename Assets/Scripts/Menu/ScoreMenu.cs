using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    private GameObject[] stars;    //array of all three star fill objects
    [Range(1, 3)] public int starRating = 3; //The star rating the player can have from 1-3 stars


    void Start()
    {
        stars = GameObject.FindGameObjectsWithTag("Star");

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }

        DisplayStarRating();
    }


    //Activate the number of stars correspond to star rating
    void DisplayStarRating()
    {
        for (int i = 0; i < starRating; i++)
        {
            stars[i].SetActive(true);
        }
    }


    //Called in menu button event
    public void MenuButton()
    {
        AudioManager.instance.PlaySFX("Click", true);
        SceneManager.LoadScene("TitleScreen");
    }


    //Called in restart button event
    public void RestartButton()
    {
        AudioManager.instance.PlaySFX("Click", true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    //Called in restart button event
    public void NextButton()
    {
        AudioManager.instance.PlaySFX("Click", true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
