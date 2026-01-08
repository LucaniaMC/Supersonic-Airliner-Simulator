using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPointer : MonoBehaviour
{
    public GameObject pointer; //Prefab of the pointer sprite
    GameObject pointerInstance;
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
        float angle = Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg;
        pointerInstance.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
