using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;

    public GameObject pauseMenu;
    public PlayerController script1;

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
        script1.input = false;
    }

    public void PauseButton() 
    {
        Pause();
        FindObjectOfType<AudioManager>().PlaySFX("Click", true);
    }


    public void ResumeButton() 
    {
        FindObjectOfType<AudioManager>().PlaySFX("Click", true);
        script1.input = true;
        Resume();
    }

    public void MenuButton() 
    {
        Resume();
        FindObjectOfType<AudioManager>().PlaySFX("Click", true);
        SceneManager.LoadScene("TitleScreen");
    }

    public void RestartButton()
    {
        Resume();
        FindObjectOfType<AudioManager>().PlaySFX("Click", true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
