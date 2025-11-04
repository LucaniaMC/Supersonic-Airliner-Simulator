using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform player;


    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }


    void Update()
    {
        transform.position = player.transform.position;
    }
}
