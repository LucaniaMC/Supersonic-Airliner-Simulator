using UnityEngine;

public class PlayerInitialization : MonoBehaviour
{
    public PlayerController controller;
    public FuelBar fuelBar;
    public PlayerShadow shadow;
    public PlayerInitialization self;
    public GameObject launch;
    public GameObject overlay;
    
    void Awake()
    {
        controller.enabled = false;
        fuelBar.enabled = false;
        shadow.enabled = false;
        Invoke("DisableOverlay", 1f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            FindObjectOfType<AudioManager>().Play("BoostStart");
            controller.enabled = true;
            fuelBar.enabled = true;
            shadow.enabled = true;
            controller.input = true;
            self.enabled = false;
            launch.SetActive(true);
        }  
    } 

    void DisableOverlay() 
    {
        overlay.SetActive(false);
    }
}
