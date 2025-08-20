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

    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject goal;
    private FuelBar fuelBar;

    bool hasrun = false; //Stupid way to make the code run only once

    private GameObject overlay;
    private Animator overlayAnimator;


    void Start()
    {
        fuelBar = GameObject.FindObjectOfType<FuelBar>();
        goal = GameObject.FindWithTag("Finish");
        player = GameObject.FindWithTag("Player");
        overlay = GameObject.Find("Overlay");
        overlayAnimator = overlay.GetComponent<Animator>();
        AudioManager.instance.PlayMusic(BGMName);
    }


    public void Finish()
    {
        fuelBar.enabled = false;
        overlay.SetActive(true);

        hasrun = true;
        overlayAnimator.SetBool("Finish", true);

        Invoke("NextScene", 2f);
    }


    void NextScene()
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
            fuelBar.enabled = false;

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
