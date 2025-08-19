using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    public Slider slider;

    public int fuel = 100;

    float time = 0f;
    float delay;

    public PlayerFinishDeath other; //Get OnDeath method from the other script

    void Update() 
    {
        slider.value = fuel;
        
        //Fuel Decrease over time Timer
        time += Time.deltaTime; 

        if (time >= delay)
        {
            time = 0f;
            fuel--;
        }

        //Change of speed
        if (Input.GetMouseButton(0))
        {
            delay = 1f;
        }
        else
        {
            delay = 0.5f;
        }

        //Out of fuel death
        if (fuel <= 0) 
        {
            slider.value = 0;
            other.OnDeath();
        }
    }
}
