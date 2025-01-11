using UnityEngine;

public class Intro : MonoBehaviour
{
    public PlayerController script1;
    public FuelBar script2;
    public PlayerShadow script3;
    public Intro self;
    public GameObject launch;
    public GameObject overlay;
    
    void Awake()
    {
        script1.enabled = false;
        script2.enabled = false;
        script3.enabled = false;
        Invoke("DisableOverlay", 1f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            FindObjectOfType<AudioManager>().Play("BoostStart");
            script1.enabled = true;
            script2.enabled = true;
            script3.enabled = true;
            script1.input = true;
            self.enabled = false;
            launch.SetActive(true);
        }  
    } 

    void DisableOverlay() 
    {
        overlay.SetActive(false);
    }
}
