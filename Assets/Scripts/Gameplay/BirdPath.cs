using UnityEngine;

//For birds to patrol back and forth between its initial position and the position of a target object
public class BirdPath : MonoBehaviour
{
    public enum SpeedMode { Time, Speed }
    public enum PathType { Line, Circle }

    public SpeedMode speedMode = SpeedMode.Time;
    public PathType pathType = PathType.Line;   //Moves in a line or circle


    public float speed = 1f;    //for Time mode how many loops it'll complete per second, for Speed mode is speed per second
    public float offset; //Start time offset

    [Header ("Circle Mode Parameters")]
    public float radius;
    public bool counterClockwise;

    [Header ("References")]
    public GameObject target;       //target that the bird moves to
    public SpriteRenderer sprite;   //bird's sprite renderer
    public GameObject bird;         //the bird that's moving
    public Animator animator;       //animator of the bird

    Vector3 pos1; //Origin position
    Vector3 pos2; //Target position

    //For bird flip
    float lastpos;
    float journeyLength;
    bool facingLeft;


    void Start()
    {
        pos1 = transform.position;
        pos2 = target.transform.position;

        journeyLength = Vector3.Distance(pos1, pos2);
    }

    void Update()
    {
        if (bird == null) return;

        if (pathType == PathType.Line)
        {
            if (speedMode == SpeedMode.Time)
            {
                //Bird moves back and forth
                bird.transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed + offset, 1f));
            }

            if (speedMode == SpeedMode.Speed)
            {
                if (journeyLength <= 0.001f)
                {
                    bird.transform.position = pos1;
                    return;
                }

                // Base distance traveled so far
                float distanceCovered = Time.time * speed;

                // Apply offset as fraction of journey length
                distanceCovered += offset * journeyLength;

                // PingPong across the journey length to move back and forth
                float fractionOfJourney = Mathf.PingPong(distanceCovered, journeyLength) / journeyLength;

                bird.transform.position = Vector3.Lerp(pos1, pos2, fractionOfJourney);
            }
        }
        
        if(pathType == PathType.Circle)
        {
            if (radius <= 0.001f) return;

            float angle;

            if (speedMode == SpeedMode.Time)
            {
                angle = (Time.time * speed + offset) * Mathf.PI * 2f;
            }
            else // Speed mode
            {
                float circumference = 2f * Mathf.PI * radius;
                float distance = Time.time * speed + offset * circumference;
                angle = distance / radius; 
            }

            if (!counterClockwise) angle = -angle;

            float x = pos1.x + Mathf.Cos(angle) * radius;
            float y = pos1.y + Mathf.Sin(angle) * radius;

            bird.transform.position = new Vector3(x, y, bird.transform.position.z);
        }


        //Flips asset
        if (bird.transform.position.x < lastpos)
        {
            if (facingLeft != true)
            {
                facingLeft = true;
                animator.SetTrigger("Turn");
            }
            sprite.flipX = false;
        }

        else if (bird.transform.position.x > lastpos)
        {
            if (facingLeft != false)
            {
                facingLeft = false;
                animator.SetTrigger("Turn");
            }
            sprite.flipX = true;
        }

        lastpos = bird.transform.position.x;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        // Draw line path
        if (pathType == PathType.Line)
            Gizmos.DrawLine(transform.position, target.transform.position);

        // Draw circle path
        if (pathType == PathType.Circle)
            Gizmos.DrawWireSphere(transform.position, radius);
    }
}
