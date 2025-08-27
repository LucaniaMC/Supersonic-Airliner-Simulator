using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : UIMenu
{

    void Start()
    {
        Hide();
    }


    //Toggle between pause and unpause when pressing ESC
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && LevelManager.instance.IsInLevel())
        {
            if (LevelManager.paused)
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
    public void Resume()
    {
        Hide();
        LevelManager.instance.Resume();
    }


    public void Pause()
    {
        Show();
        LevelManager.instance.Pause();
    }
    #endregion


    #region Button Functions
    //Called in menu button event
    public void MenuButton()
    {
        Resume();
        SceneManager.LoadScene("TitleScreen");
    }


    //Called in restart button event
    public void RestartButton()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
