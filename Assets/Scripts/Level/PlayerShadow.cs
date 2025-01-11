using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public GameObject player;

    public Vector3 offset;

    void Update()

    {
        transform.position = player.transform.position + offset;
        transform.rotation = player.transform.rotation;
    }
}
