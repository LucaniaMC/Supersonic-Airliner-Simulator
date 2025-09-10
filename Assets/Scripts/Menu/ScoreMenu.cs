using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreMenu : UIMenu
{
    [SerializeField] private GameObject[] stars;    //array of all three star fill objects

    [Range(1, 3)] public int starRating = 3; //The star rating the player can have from 1-3 stars


    [SerializeField] TextMeshProUGUI fuelText;
    [SerializeField] TextMeshProUGUI timeText;


    public void Initialize()
    {
        //Disable all stars
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }

        //calculate and displays star rating
        starRating = CalculateStarRating(LevelManager.instance.fuelRemaining);
        Debug.Log("Star Rating:" + starRating);
        DisplayStarRating();

        //displays stats in texts
        DisplayFuel();
        DisplayTimeTaken();
    }


    //Returns star rating stats in the range of 1-3 depending on player's fuel remaining
    int CalculateStarRating(int score)
    {
        if (score >= LevelManager.instance.threeStarThreshold)
        {
            return 3;
        }
        else if (score >= LevelManager.instance.twoStarThreshold)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }


    //Activate the number of stars correspond to star rating
    void DisplayStarRating()
    {
        for (int i = 0; i < starRating; i++)
        {
            stars[i].SetActive(true);
        }
    }


    //displays fuel in text as percentage
    void DisplayFuel()
    {
        fuelText.text = LevelManager.instance.fuelRemaining + "%";
    }


    //displays time in text in minute:second format
    void DisplayTimeTaken()
    {
        int minutes = Mathf.FloorToInt(LevelManager.instance.timeTaken / 60);
        int seconds = Mathf.FloorToInt(LevelManager.instance.timeTaken % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    //Called in menu button event
    public void MenuButton()
    {
        SceneManager.LoadScene("TitleScreen");
    }


    //Called in restart button event
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    //Called in restart button event
    public void NextButton()
    {
        LevelManager.instance.NextScene();
    }
}
