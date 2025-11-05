using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public Transform player;

    public Vector2 offset;

    public bool isActive = false;

    void Update()
    {
        transform.rotation = player.transform.rotation;

        if (isActive)
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
    }
}
