using UnityEngine;

//For birds to patrol back and forth between its initial position and the position of a target object
public class BirdPath : MonoBehaviour
{
    public float speed = 1f; //Speed
    Vector3 pos1; //Position 1
    Vector3 pos2; //Position 2
    [Range (0f, 1f)] public float offset; //Start time offset

    //For bird flip
    float lastpos;
    public SpriteRenderer sprite;
    public GameObject target;

    float journeyLength;


    void Start()
    {
        pos1 = transform.position;
        pos2 = target.transform.position;

        journeyLength = Vector3.Distance(pos1, pos2);
    }

    void Update()
    {
        //If bird start and ending position is the same
        if (journeyLength <= 0.001f)
        {
            transform.position = pos1;
            return;
        }

        // Base distance traveled so far
        float distanceCovered = Time.time * speed;

        // Apply offset as fraction of journey length
        distanceCovered += offset * journeyLength;

        // PingPong across the journey length to move back and forth
        float fractionOfJourney = Mathf.PingPong(distanceCovered, journeyLength) / journeyLength;

        transform.position = Vector3.Lerp(pos1, pos2, fractionOfJourney);

        // Flips asset
        if (transform.position.x < lastpos)
        {
            sprite.flipX = false;
        }
        else if (transform.position.x > lastpos)
        {
            sprite.flipX = true;
        }

        lastpos = transform.position.x;
    }
}
