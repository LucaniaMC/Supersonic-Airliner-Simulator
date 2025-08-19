using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFinishDeath : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other) 
    {
        //Reach goal finish game
        if (other.tag == "Finish") 
        {
            OnFinish();
        }

        //Death zone death
        if (other.tag == "DeathZone") 
        {
            OnDeath();
        }
    }

    public Animator animator;

    //Finish event

    public PlayerController controller;
    public FuelBar fuelBar;
    public GameObject confetti;


    //Assign game managers
    void Start()
    {
        //confetti = GameObject.Find("Confetti"); //this is not working on inactive objects
        fuelBar = FindObjectOfType<FuelBar>();
        controller = FindObjectOfType<PlayerController>();
    }


    void OnFinish() 
    {
        if (controller.input == true) 
        {
            controller.enabled = false;
            fuelBar.enabled = false;
            overlay.SetActive(true);
        
            controller.audioManager.ToggleLoopingSFX("BoostLoop", false); //Fix boost audio continue playing when finished
            //This is now causing a null reference error
            hasrun = true; //Set death to true to fix death happening after victory

            confetti.SetActive(true);
            animator.SetBool("Finish",true);

            controller.audioManager.PlaySFX("Finish", false);

            Invoke ("NextScene", 2f); 
        }
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

    //Death event

    bool hasrun = false; //Stupid way to make the code run only once
    public GameObject overlay;
    
    public void OnDeath() 
    {
        if (hasrun == false) 
        {
            overlay.SetActive(true);
            animator.SetBool("OnDeath",true);
            fuelBar.enabled = false;
            controller.input = false; //Disable player input in PlayerController
        
            Invoke ("ReloadScene", 2f);  

            controller.audioManager.PlaySFX("Fail", false); //Failsound
            hasrun = true;
        }
    }

    void ReloadScene() 
    {
        Scene reloadscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(reloadscene.name);
    }   
}
