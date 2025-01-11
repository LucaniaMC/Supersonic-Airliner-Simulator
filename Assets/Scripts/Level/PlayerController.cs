using UnityEngine;


public class PlayerController : MonoBehaviour
{
    
    public GameObject player;

    public AudioSource boostsound; //Put this in audiomanager

    //For player movements
    float moveSpeed = 0f;
    Vector3 target;

    [HideInInspector] public bool input = true; //For disabling input


    //Generate Sonic Boom
    public GameObject boom;

    float time = 0f;
    float delay = 0.1f;
    

    void Update()
    {
        //Switch between speeds when holding down left mouse button
        if (Input.GetMouseButton(0) & input == true) 
        {
           moveSpeed = 3f;
           boostsound.mute = false;


           //Generate Sonic Boom
           time = time + 1f * Time.deltaTime;

            if (time >= delay) 
            {
                time = 0f;
                Instantiate(boom, player.transform.position, Quaternion.identity); 
            }
        }
        else
        {
            moveSpeed = 1.5f;
            boostsound.mute = true;
        }


        //Play sound without repeating
        if (Input.GetMouseButtonDown(0) & input == true) 
        {
            FindObjectOfType<AudioManager>().Play("BoostStart");
        }


        //Move towards mouse position
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        player.transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed*Time.deltaTime);

        //Rotate towards mouse position
        Vector3 direction = target - player.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 


        //Camera follow
        Camera.main.transform.position = player.transform.position;
    } 
}
