using UnityEngine;


public class PlayerController : MonoBehaviour
{
    
    public GameObject player;

    //For player movements
    float moveSpeed = 0f;
    Vector3 target;

    [HideInInspector] public bool input = true; //For disabling input

    //Generate Sonic Boom
    public GameObject boom;

    float time = 0f;
    float delay = 0.1f;

    [HideInInspector] public AudioManager audioManager;


    //Assign game managers
    void Start() 
    {
        audioManager = FindObjectOfType<AudioManager>();
    }


    void Update()
    {
        
    } 
}
