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
    [Range(0f, 360f)] public float windAngle = 0f;      // wind angle in degrees
    public float windStrength = 0f;              // speed of wind

    //references
    private PlayerStateMachine player;
    private Camera mainCamera;


    void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
        mainCamera = Camera.main;
    }


    public void MoveTowardsCursor()
    {
        // Get mouse position in world space
        target = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z; // keep same z depth

        // Move toward the mouse direction
        Vector3 targetDirection = (target - transform.position).normalized;
        Vector3 newPos = player.transform.position + targetDirection * moveSpeed * Time.deltaTime;

        //only calculates wind if it exists
        if (windStrength > 0f)
        {
            //Calculate wind direction as Vector2 from wind angle
            Vector2 windDirection = new Vector2(Mathf.Cos(windAngle * Mathf.Deg2Rad), Mathf.Sin(windAngle * Mathf.Deg2Rad));

            //Add wind offset
            Vector3 windOffset = Time.deltaTime * windStrength * windDirection.normalized;
            newPos += windOffset;
        }

        //Calculate black hole pulls
        foreach (BlackHole blackHole in FindObjectsOfType<BlackHole>())
        {
            Vector3 pull = blackHole.GetPullForce(player.transform.position);
            newPos += pull * Time.deltaTime;
        }

        //Calculate composite player position to move to
        player.transform.position = newPos;

        //Rotate towards mouse position
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0f, 0f, angle);
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

    
    public void SetWind(float angle, float strength)
    {
        windAngle = angle;
        windStrength = strength;

        if (strength > 0)
        {
            player.windParticles.SetRotation(angle);
            player.windParticles.SetSpeed(strength);
            player.windParticles.ActivateWind(true);
        }
        else
        {
            player.windParticles.ActivateWind(false);
        }
    }
}
