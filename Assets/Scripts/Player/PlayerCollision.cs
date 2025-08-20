using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [HideInInspector] public LevelManager levelManager;


    void Start() 
    {
        levelManager = FindObjectOfType<LevelManager>();
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        //Reach goal finish game
        if (other.tag == "Finish" && levelManager.failed == false) 
        {
            levelManager.finished = true;
        }

        //Death zone death
        if (other.tag == "DeathZone" && levelManager.finished == false) 
        {
            levelManager.failed = true;
        }
    }
}
