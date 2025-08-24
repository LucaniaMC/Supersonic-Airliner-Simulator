using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreMenu : MonoBehaviour
{
    private GameObject[] stars;    //array of all three star fill objects
    private LevelManager levelManager;

    [Range(1, 3)] public int starRating = 3; //The star rating the player can have from 1-3 stars

    //Threshold for star rating
    public int threeStarThreshold = 50; //fuel needed for 3 stars
    public int twoStarThreshold = 25;   //fuel needed for 2 stars


    [SerializeField] TextMeshProUGUI fuelText;
    [SerializeField] TextMeshProUGUI timeText;


    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        stars = GameObject.FindGameObjectsWithTag("Star");

        //Disable all stars
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }

        //calculate and displays star rating
        starRating = CalculateStarRating(levelManager.fuelRemaining);
        Debug.Log("Star Rating:" + starRating);
        DisplayStarRating();

        //displays stats in texts
        DisplayFuel();
        DisplayTimeTaken();
    }


    //Returns star rating stats in the range of 1-3 depending on player's fuel remaining
    int CalculateStarRating(int score)
    {
        if (score >= threeStarThreshold)
        {
            return 3;
        }
        else if (score >= twoStarThreshold)
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
        fuelText.text = levelManager.fuelRemaining + "%";
    }


    //displays time in text in minute:second format
    void DisplayTimeTaken()
    {
        int minutes = Mathf.FloorToInt(levelManager.timeTaken / 60);
        int seconds = Mathf.FloorToInt(levelManager.timeTaken % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
        levelManager.NextScene();
    }
}
