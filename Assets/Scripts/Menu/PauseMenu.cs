using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;

    public GameObject pauseMenu;
    public PlayerController script1;

    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
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
        audioManager.PlaySFX("Click", true);
    }


    public void ResumeButton() 
    {
        audioManager.PlaySFX("Click", true);
        script1.input = true;
        Resume();
    }

    public void MenuButton() 
    {
        Resume();
        audioManager.PlaySFX("Click", true);
        SceneManager.LoadScene("TitleScreen");
    }

    public void RestartButton()
    {
        Resume();
        audioManager.PlaySFX("Click", true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
