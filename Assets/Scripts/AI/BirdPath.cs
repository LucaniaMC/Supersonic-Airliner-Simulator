using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//For birds to patrol back and forth between its initial position and the position of a target object
public class BirdPath : MonoBehaviour
{
    public float speed = 1f; //Speed
    Vector3 pos1; //Position 1
    Vector3 pos2; //Position 2
    public float offset; //Start time offset

    //For bird flip
    float lastpos;
    public SpriteRenderer sprite;

    public GameObject target;

    void Start()
    {
        pos1 = transform.position;
        pos2 = target.transform.position;
    }

    void Update()
    {
        //Bird moves back and forth
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed + offset, 1f));


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
