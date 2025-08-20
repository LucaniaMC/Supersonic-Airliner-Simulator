using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public GameObject player;

    public Vector3 offset;

    public bool isActive = false;

    void Update()
    {
        transform.rotation = player.transform.rotation;

        if (isActive)
        {
            transform.position = player.transform.position + offset;
        }
        else
        {
            transform.position = player.transform.position;
        }
    }
}
