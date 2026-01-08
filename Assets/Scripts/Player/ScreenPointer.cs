using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPointer : MonoBehaviour
{
    public GameObject pointer; //Prefab of the pointer sprite
    Camera mainCamera;
    GameObject player;

    void Start()
    {
        mainCamera = Camera.main;
        player = LevelManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
