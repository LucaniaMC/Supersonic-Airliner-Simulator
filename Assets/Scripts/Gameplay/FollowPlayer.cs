using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;


    void Start()
    {
        if (player == null)
        {
            player = LevelManager.instance.player.transform;
        }
    }


    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
