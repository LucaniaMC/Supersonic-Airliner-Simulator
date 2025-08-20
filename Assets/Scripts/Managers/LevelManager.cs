using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool failed = false;     //Has the player failed
    public bool finished = false;   //Has the player reached the goal

    public bool isNight = false; //Is the level a night level
    public string BGMName;  //BGM that plays in the level

    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject goal;

    [HideInInspector] public FuelBar fuelBar;

    bool hasrun = false; //Stupid way to make the code run only once
    public GameObject overlay;
    public Animator animator;


    void Start()
    {
        fuelBar = FindObjectOfType<FuelBar>();
        goal = GameObject.FindWithTag("Finish");
        player = GameObject.FindWithTag("Player");
    }
  

    public void Finish()
    {
        fuelBar.enabled = false;
        overlay.SetActive(true);

        hasrun = true;
        animator.SetBool("Finish", true);

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
            animator.SetBool("OnDeath",true);
            fuelBar.enabled = false;
        
            Invoke ("ReloadScene", 2f);  

            hasrun = true;
        }
    }


    void ReloadScene() 
    {
        Scene reloadscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(reloadscene.name);
    }  
}
