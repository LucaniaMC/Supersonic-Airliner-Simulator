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

    void Start()
    {
        mainCamera = Camera.main;
        player = LevelManager.instance.player;
        pointerInstance = Instantiate(pointer);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotates the pointer away from player
        float angle = Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg;
        pointerInstance.transform.rotation = Quaternion.Euler(0f, 0f, angle - 180f);

        //check if the object is off screen
        var screenPos = mainCamera.WorldToViewportPoint(transform.position);
        bool isOffScreen = screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1;

        if (isOffScreen)
        {
            pointerInstance.SetActive(true);
        }
        else
        {
            pointerInstance.SetActive(false);
        }
    }
}
