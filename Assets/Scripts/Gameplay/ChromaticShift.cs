using UnityEngine;

//Controls the intensity of the chromatic shift effect for space level
//Includng an idle looping effect, and increase intensity when getting near black holes (TBA)
public class ChromaticShift : MonoBehaviour
{
    //Subtle idle effect that fades in and out
    float idleShiftAmount = 0f; //Shift amount input into the material
    float idleSpeedModifier = 2f;   //Speed for the idle loop
    float idleMaxIntensity = 0.15f;  //Maximum shift amount for the idle loop


    void Update()
    {
        //Idle Effect
        float sin = Mathf.Sin(Time.time * idleSpeedModifier);
        float clampedSin = Mathf.Clamp(sin, -0.5f, 0.5f);
        float shiftedSin = clampedSin + 0.5f;
        idleShiftAmount = shiftedSin * idleMaxIntensity;

        CameraManager.instance.SetChromaticShift(idleShiftAmount);
    }
}
