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

    public PlayerController script1;
    public FuelBar script2;
    public GameObject confetti;

    public AudioSource boostsound; //Fix boost audio continue playing when finished

    private AudioManager audioManager;


    //Assign game managers
    void Start() 
    {
        audioManager = FindObjectOfType<AudioManager>();
    }


    void OnFinish() 
    {
        if (script1.input == true) 
        {
            script1.enabled = false;
            script2.enabled = false;
            overlay.SetActive(true);
        
            audioManager.ToggleLoopingSFX("BoostLoop", false); //Fix boost audio continue playing when finished
            //This is now causing a null reference error
            hasrun = true; //Set death to true to fix death happening after victory

            confetti.SetActive(true);
            animator.SetBool("Finish",true);

            audioManager.Play("Finish");

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
            script2.enabled = false;
            script1.input = false; //Disable player input in PlayerController
        
            Invoke ("ReloadScene", 2f);  

            audioManager.Play("Fail"); //Failsound
            hasrun = true;
        }
    }

    void ReloadScene() 
    {
        Scene reloadscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(reloadscene.name);
    }   
}
