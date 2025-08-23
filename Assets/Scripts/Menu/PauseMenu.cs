using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;  //Is the game paused

    public GameObject pauseMenu;


    void Start()
    {
        pauseMenu.SetActive(false);
    }


    //Toggle between pause and unpause when pressing ESC
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    #region Pause Functions
    void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }


    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }
    #endregion


    #region Button Functions
    //Called in pause button event
    public void PauseButton()
    {
        Pause();
        AudioManager.instance.PlaySFX("Click", true);
    }


    //Called in resume button event
    public void ResumeButton()
    {
        AudioManager.instance.PlaySFX("Click", true);
        Resume();
    }


    //Called in menu button event
    public void MenuButton()
    {
        Resume();
        AudioManager.instance.PlaySFX("Click", true);
        SceneManager.LoadScene("TitleScreen");
    }


    //Called in restart button event
    public void RestartButton()
    {
        Resume();
        AudioManager.instance.PlaySFX("Click", true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
