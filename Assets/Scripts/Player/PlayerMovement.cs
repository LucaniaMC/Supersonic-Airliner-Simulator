using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //For player movements
    private float moveSpeed = 0f;   //The player's current moving speed
    readonly float normalSpeed = 1.5f;
    readonly float boostSpeed = 3f;

    //mouse position that the player points to
    Vector3 target;

    //wind
    public float windAngle {get; private set;} = 0f;      // wind angle in degrees ranging from 0-360
    public float windStrength {get; private set;} = 0f;                     // speed of wind

    //Black hole references
    BlackHole[] blackHoles; //Every black hole

    //Readable parameters
    public float distanceToBlackHoles {get; private set;}

    //references
    private PlayerStateMachine player;
    Camera cameraRig;   //Uses the camera rig for accurate mouse position during screen shakes


    void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
        cameraRig = CameraManager.instance.cameraRig.GetComponent<Camera>();

        //Get a reference for all black holes in scene
        blackHoles = FindObjectsOfType<BlackHole>();
    }


    public void MoveTowardsCursor()
    {
        // Get mouse position in world space
        target = cameraRig.ScreenToWorldPoint(Input.mousePosition);
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

        //reset distance to nearest black holes
        distanceToBlackHoles = Mathf.Infinity;

        //Calculate black hole pulls
        foreach (BlackHole blackHole in blackHoles)
        {
            //Calculate black hole pulls
            Vector3 pull = blackHole.GetPullForce(player.transform.position);
            newPos += pull * Time.deltaTime;

            //Calculate nearest black hole distance
            float distance = Vector2.Distance(player.transform.position, blackHole.transform.position);
            if (distance < distanceToBlackHoles)
            {
                distanceToBlackHoles = distance;    //loop through all black holes and keep the smallest number
            }
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
    }


    //move at default speed
    public void Move()
    {
        moveSpeed = normalSpeed;
        AudioManager.instance.ToggleLoopingSFX("BoostLoop", false);
    }

    
    public void SetWind(float angle, float strength)
    {
        windAngle = Mathf.Clamp(angle, 0, 360);
        windStrength = Mathf.Max(strength, 0f);
    }
}
