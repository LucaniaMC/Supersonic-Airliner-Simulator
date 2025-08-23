using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //For player movements
    private float moveSpeed = 0f;
    readonly float normalSpeed = 1.5f;
    readonly float boostSpeed = 3f;

    //for sonic boom timer
    float time = 0f;
    readonly float delay = 0.1f;

    //mouse position that the player points to
    Vector3 target;

    //wind
    public Vector2 windDirection = Vector2.zero; // normalized direction
    public float windStrength = 0f;              // speed of wind

    //references
    public PlayerStateMachine player;
    private Camera mainCamera;


    void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
        mainCamera = Camera.main;
    }


    public void MoveTowardsCursor()
    {
        //mouse target position
        target = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //Player's own movement
        Vector3 newPos = Vector3.MoveTowards(player.transform.position, target, moveSpeed * Time.deltaTime);

        //Add wind offset
        Vector3 windOffset = Time.deltaTime * windStrength * windDirection.normalized;
        newPos += windOffset;

        //Calculate composite player position to move to
        player.transform.position = newPos;

        //Rotate towards mouse position
        Vector3 direction = target - player.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Camera follow
        mainCamera.transform.position = player.transform.position;
    }


    //move at supersonic speed
    public void SonicBoost()
    {
        moveSpeed = boostSpeed;
        AudioManager.instance.ToggleLoopingSFX("BoostLoop", true);

        SpawnSonicBoom();
    }

    //move at default speed
    public void Move()
    {
        moveSpeed = normalSpeed;
        AudioManager.instance.ToggleLoopingSFX("BoostLoop", false);
    }


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
