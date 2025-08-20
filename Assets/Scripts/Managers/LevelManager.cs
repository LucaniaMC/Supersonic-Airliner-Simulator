using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool failed = false;
    public bool finished = false;

    public bool isNight = false;

    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject goal;

    [HideInInspector] public AudioManager audioManager;
    [HideInInspector] public FuelBar fuelBar;

    bool hasrun = false; //Stupid way to make the code run only once
    public GameObject overlay;
    public Animator animator;


    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        fuelBar = FindObjectOfType<FuelBar>();
        goal = GameObject.FindWithTag("Finish");
        player = GameObject.FindWithTag("Player");
    }


    //Fix for audio manager not referenced correctly
    void Update()
    {
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
    }
    

    public void Finish()
    {
        fuelBar.enabled = false;
        overlay.SetActive(true);

        audioManager.ToggleLoopingSFX("BoostLoop", false); //Fix boost audio continue playing when finished
        //This is now causing a null reference error
        hasrun = true; //Set death to true to fix death happening after victory
        animator.SetBool("Finish", true);

        audioManager.PlaySFX("Finish", false);

        Invoke("NextScene", 2f);
    }


    void NextScene()
    {
        //if in test scene, reload the scene
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            Invoke("ReloadScene", 2f);
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

            audioManager.PlaySFX("Fail", false); //Failsound
            hasrun = true;
        }
    }


    void ReloadScene() 
    {
        Scene reloadscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(reloadscene.name);
    }  
}
