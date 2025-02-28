using UnityEngine;

//https://discussions.unity.com/t/smooth-movement-along-waypoints-update-vs-fixedupdate/165974
public class RandomMovement : MonoBehaviour
{
    Vector3 randomTarget;
    Vector3 center;
    public Vector2 range;
    public float turnSpeed;
    public float speed;

    void Start() 
    {
        center = transform.position;
        randomTarget = center + new Vector3(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y), transform.position.z);
    }


    void Update()
    {
        //Moves at a constant rate
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        float distance = Vector3.Distance (transform.position, randomTarget);

        if(distance >= 0.1f) 
        {
            Vector3 direction = randomTarget - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), turnSpeed * Time.deltaTime);
        } 

        //When approaching target, switch to a new target
        //If distance and turn speed is too low, it'll keep spinning without changing target
        if (distance < 1f)
        {
            randomTarget = center + new Vector3(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y), transform.position.z);
        }
    }

}
