using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;

    public GameObject pauseMenu;


    void Start()
    {
        pauseMenu.SetActive(false);
    }
    
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

    public void PauseButton() 
    {
        Pause();
        AudioManager.instance.PlaySFX("Click", true);
    }


    public void ResumeButton() 
    {
        AudioManager.instance.PlaySFX("Click", true);
        Resume();
    }

    public void MenuButton() 
    {
        Resume();
        AudioManager.instance.PlaySFX("Click", true);
        SceneManager.LoadScene("TitleScreen");
    }

    public void RestartButton()
    {
        Resume();
        AudioManager.instance.PlaySFX("Click", true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
