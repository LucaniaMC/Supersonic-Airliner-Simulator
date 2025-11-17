using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    public int fuel { get; private set; } = 100;

    float time = 0f;

    [SerializeField] Gradient gradient; //Colors that the slider change to when decreasing
    [SerializeField] Image fill; //Reference to the slider image to change its color


    void Start()
    {
        UpdateFuel();
    }


    //Decrease the fuel over time, delay is 1 when boosting, and 0.5 when not boosting
    public void DecreaseFuel(float delay)
    {
        //Fuel Decrease over time Timer
        time += Time.deltaTime;

        if (time >= delay)
        {
            time = 0f;
            fuel--;
        }

        UpdateFuel();
    }


    //Increase the fuel for a given amount
    public void IncreaseFuel(int amount)
    {
        fuel += amount;
        UpdateFuel();
    }


    public void ResetFuel()
    {
        time = 0f;
        fuel = 100;
        UpdateFuel();
    }


    //Update slider appearance
    void UpdateFuel()
    {
        //change slider value
        slider.value = fuel;

        //Out of fuel death
        if (fuel <= 0)
        {
            slider.value = 0;
        }

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
