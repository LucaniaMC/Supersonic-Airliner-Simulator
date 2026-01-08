using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this script to an object to create a pointer towards it from the player when the object is off screen
public class ScreenPointer : MonoBehaviour
{
    public GameObject pointer; //Prefab of the pointer
    GameObject pointerInstance; //Active instance of the pointer
    Camera mainCamera;
    GameObject player;
    float offset = 0f;

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

        if (isOffScreen)
        {
            pointerInstance.SetActive(true);

            //position the pointer on screen edge between the player and the target object
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, mainCamera.ViewportToWorldPoint(Vector3.zero).x, mainCamera.ViewportToWorldPoint(Vector3.one).x);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, mainCamera.ViewportToWorldPoint(Vector3.zero).y, mainCamera.ViewportToWorldPoint(Vector3.one).y);
            pointerInstance.transform.position = clampedPosition;
        }
        else
        {
            pointerInstance.SetActive(false);
        }
    }
}
