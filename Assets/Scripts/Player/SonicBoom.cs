using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicBoom : MonoBehaviour
{

    //Self destruct after 0.75 sec
    private IEnumerator CountdownDeath()
    {
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(CountdownDeath());  
    }
    

    // Sonic boom collision with house
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "House")
        {
            collision = true;  
        }
    }

    //Call OnDeath from Player death script
    //I hate how this works
    bool collision = false;

    void Update() 
    {
        if (collision == true) 
        {
            GameObject other = GameObject.Find("Player");
            other.GetComponent<PlayerFinishDeath>().OnDeath();
            collision = false;
        }
    }
}
