using UnityEngine;

//For birds to patrol back and forth between its initial position and the position of a target object
public class BirdPath : MonoBehaviour
{
    public enum PathMode { Time, Speed } 

    public PathMode pathMode = PathMode.Time;
    public float speed = 1f;    //for Time mode this is time it takes to travel the distance, for Speed mode is speed per second

    public float offset; //Start time offset

    //references
    public SpriteRenderer sprite;   //bird's sprite renderer
    public GameObject target;       //target that the bird moves to
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

        if (pathMode == PathMode.Time)
        {
            //Bird moves back and forth
            bird.transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed + offset, 1f));
        }

        if (pathMode == PathMode.Speed)
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
        Gizmos.DrawLine(transform.position, target.transform.position);
    }
}
