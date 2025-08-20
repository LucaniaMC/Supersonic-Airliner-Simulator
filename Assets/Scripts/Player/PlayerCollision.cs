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
        if (other.tag == "Finish" && levelManager.status == LevelManager.LevelStatus.InProgress) 
        {
            levelManager.status = LevelManager.LevelStatus.Finished;
        }

        //Death zone death
        if (other.tag == "DeathZone" && levelManager.status == LevelManager.LevelStatus.InProgress) 
        {
            levelManager.status = LevelManager.LevelStatus.Failed;
        }
    }
}
