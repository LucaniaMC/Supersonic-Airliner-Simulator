using UnityEngine;
using UnityEngine.SceneManagement;

//Controls the intensity of the chromatic shift effect for space level
//Includng an idle looping effect, and increase intensity when getting near black holes (TBA)
public class SpaceGlow : MonoBehaviour
{
    //Subtle idle effect that fades in and out
    float minIntensity = 0.2f; 
    float maxIntensity = 0.4f;
    float idleSpeedModifier = 0.5f;   //Speed for the idle loop


    void FixedUpdate()
    {
        //Idle Effect
        float sin = Mathf.Sin(Time.time * idleSpeedModifier);
        float multipliedSin = sin * (maxIntensity - minIntensity);
        float shiftedSin = multipliedSin + minIntensity + 0.5f;

        ShaderManager.instance.SetGlow(shiftedSin);
    }
}
