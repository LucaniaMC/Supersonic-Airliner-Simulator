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
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
