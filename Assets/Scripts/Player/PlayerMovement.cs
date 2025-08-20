using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //For player movements
    public float moveSpeed = 0f;
    public Vector3 target;
    public PlayerStateMachine player;

    void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
    }


    public void MoveTowardsCursor()
    {
        //Move towards mouse position
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        player.transform.position = Vector3.MoveTowards(player.transform.position, target, moveSpeed * Time.deltaTime);

        //Rotate towards mouse position
        Vector3 direction = target - player.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //Camera follow
        Camera.main.transform.position = player.transform.position;
    }


    //move at supersonic speed
    public void SonicBoost()
    {
        moveSpeed = 3f;
        AudioManager.instance.ToggleLoopingSFX("BoostLoop", true);

        SpawnSonicBoom();
    }

    //move at default speed
    public void Move()
    {
        moveSpeed = 1.5f;
        AudioManager.instance.ToggleLoopingSFX("BoostLoop", false);
    }


    //for sonic boom timer
    float time = 0f;
    float delay = 0.1f;

    void SpawnSonicBoom()
    {
        time = time + 1f * Time.deltaTime;

        if (time >= delay)
        {
            time = 0f;
            GameObject.Instantiate(player.boom, player.transform.position, Quaternion.identity);
        }
    }
}
