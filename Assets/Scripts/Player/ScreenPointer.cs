using UnityEngine;

// Attach this script to an object to create a pointer towards it from the player when the object is off screen
public class ScreenPointer : MonoBehaviour
{
    public GameObject pointer; //Prefab of the pointer
    GameObject pointerInstance; //Active instance of the pointer
    Camera mainCamera;
    GameObject player;
    private readonly Plane[] planes = new Plane[6];


    void Start()
    {
        mainCamera = Camera.main;
        player = LevelManager.instance.player;
        pointerInstance = Instantiate(pointer);
    }


    void Update()
    {
        //Rotates from the player towards target
        float angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        pointerInstance.transform.rotation = Quaternion.Euler(0f, 0f, angle - 180f);

        //check if the object is off screen
        var screenPos = mainCamera.WorldToViewportPoint(transform.position);
        bool isOffScreen = screenPos.x <= 0 || screenPos.x >= 1 || screenPos.y <= 0 || screenPos.y >= 1;

        //Activates pointer when the target is off screen
        if (isOffScreen)
        {
            pointerInstance.SetActive(true);

            //position the pointer on screen edge between the player and the target object
            Vector3 origin = player.transform.position;
            Vector3 direction = transform.position - player.transform.position;

            var ray = new Ray(origin, direction);

            var closestDistance = float.PositiveInfinity;
            var hitPoint = Vector3.zero;
            GeometryUtility.CalculateFrustumPlanes(mainCamera, planes);
            for (var i = 0; i < 4; i++)
            {
                // Raycast against the plane
                if (planes[i].Raycast(ray, out var distance))
                {
                    // Keep the closest hit
                    if (distance < closestDistance)
                    {
                        hitPoint = ray.origin + ray.direction * distance;
                        closestDistance = distance;
                    }
                }
            } //https://stackoverflow.com/questions/63034454/unity-get-point-on-edge-of-the-screen-that-object-directed-to

            //position the pointer at the hitpoint
            pointerInstance.transform.position = hitPoint;

        }
        else //Deactivates pointer when the target is on screen
        {
            pointerInstance.SetActive(false);
        }
    }
}
