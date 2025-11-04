using UnityEngine;

//attach this script to an object for it to move in parallax
public class Parallax : MonoBehaviour
{
    //How much the object moves in parallax, 0 = no parallax, 0-1 = background, -1-0 = foreground
    [Range(-1f, 1f)] public float parallaxPercentage;
    public bool useOffset = true;

    Transform cameraTransform;    //Camera transform reference
    Vector3 startPos;             //Object starting position
    private Vector3 cameraStartPos; //Camera starting position

    Vector3 originOffset;  //offset the object at start, so it appears at its original position as the player approaches 
    Vector3 cameraDelta;   //distance that the camera traveled


    void Start()
    {
        //set references
        cameraTransform = Camera.main.transform;
        startPos = transform.position;
        cameraStartPos = cameraTransform.position;

        //calculates offset if it's on
        if (useOffset)
        {
            originOffset = (transform.position - cameraTransform.position) * parallaxPercentage;
            originOffset.z = 0;
        }
        else
        {
            originOffset = Vector3.zero;
        }
    }


    void Update()
    {
        cameraDelta = cameraTransform.position - cameraStartPos;
        Vector3 parallaxOffset = cameraDelta * parallaxPercentage;

        transform.position = startPos + new Vector3(parallaxOffset.x, parallaxOffset.y, 0) - originOffset;
    }
}