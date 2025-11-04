using UnityEngine;

//Attach the script with a Collider2D for it to change wind on player enter/exit
[RequireComponent(typeof(Collider2D))]
public class WindZone : MonoBehaviour
{
    public float windAngle;
    public float windStrength;

    public bool resetOnExit = false;

    PlayerMovement player;


    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.SetWind(windAngle, windStrength);
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && resetOnExit == true)
        {
            player.SetWind(0f, 0f);
        }
    }
}
