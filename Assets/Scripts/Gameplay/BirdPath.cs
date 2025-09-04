using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//For birds to patrol back and forth between its initial position and the position of a target object
public class BirdPath : MonoBehaviour
{
    public enum PathMode { Time, Speed } 

    public PathMode pathMode = PathMode.Time;
    public float speed = 1f;    //for Time mode this is time it takes to travel the distance, for Speed mode is speed per second
    Vector3 pos1; //Position 1
    Vector3 pos2; //Position 2
    public float offset; //Start time offset

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
        if (pathMode == PathMode.Time)
        {
            //Bird moves back and forth
            transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed + offset, 1f));
        }

        if (pathMode == PathMode.Speed)
        { 
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
        }


        //Flips asset
        if (transform.position.x < lastpos)
        {
            sprite.flipX = false;
            lastpos = transform.position.x;
        }

        else if (transform.position.x > lastpos)
        {
            sprite.flipX = true;
            lastpos = transform.position.x;
        }
    }

}
