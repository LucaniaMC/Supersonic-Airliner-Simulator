using UnityEngine;
using UnityEngine.SceneManagement;

//Manages dynamic level information, place the prefab in scenes
public class LevelManager : MonoBehaviour
{
    public enum LevelStatus
    {
        InProgress,
        Failed,
        Finished
    }

    public LevelStatus status = LevelStatus.InProgress; //Current status of the level
    public bool isNight = false; //Is the level a night level
    public string BGMName;  //BGM that plays in the level

    //Player stats on completion
    public int fuelRemaining = 0;
    public float timeTaken = 0f;

    public GameObject player { get; private set; }
    public GameObject goal { get; private set; }
    private FuelBar fuelBar;

    bool hasrun = false; //Stupid way to make the code run only once

    private GameObject overlay;
    private Animator overlayAnimator;
    [SerializeField] private GameObject scoreMenu;


    void Start()
    {
        goal = GameObject.FindWithTag("Finish");
        player = GameObject.FindWithTag("Player");
        fuelBar = FindObjectOfType<FuelBar>();
        overlay = GameObject.Find("Overlay");
        overlayAnimator = overlay.GetComponent<Animator>();
        AudioManager.instance.PlayMusic(BGMName);
    }


    public void Finish()
    {
        overlay.SetActive(true);

        hasrun = true;
        //overlayAnimator.SetBool("Finish", true);

        scoreMenu.SetActive(true);
        timeTaken = Time.timeSinceLevelLoad;
        fuelRemaining = fuelBar.fuel;

        Debug.Log("Fuel: " + fuelRemaining + ", " + "Time: " + timeTaken);

        //Invoke("NextScene", 2f);
    }


    public void NextScene()
    {
        //if in test scene, reload the scene
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            Invoke("ReloadScene", 0f);
            return;
        }
        //if next scene index doesn't exist, go back to title screen
        if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene("TitleScreen");
        }
        else    //load next scene
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    public void Fail()
    {
        if (hasrun == false)
        {
            overlay.SetActive(true);
            overlayAnimator.SetBool("OnDeath", true);

            Invoke("ReloadScene", 2f);

            hasrun = true;
        }
    }


    void ReloadScene()
    {
        Scene reloadscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(reloadscene.name);
    }
}
