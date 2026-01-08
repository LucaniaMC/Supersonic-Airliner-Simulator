using UnityEngine;

// Aligns the shadow with the player's rotation and position with an optional offset, attached to the shadow sprite
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
