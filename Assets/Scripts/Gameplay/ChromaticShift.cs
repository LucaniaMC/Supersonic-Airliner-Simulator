using UnityEngine;

//Controls the intensity of the chromatic shift effect for space level
//Includng an idle looping effect, and increase intensity when getting near black holes
public class ChromaticShift : MonoBehaviour
{
    //Subtle idle effect that fades in and out
    float idleShiftAmount = 0f; //Shift amount input into the material
    float idleSpeedModifier = 2f;   //Speed for the idle loop
    float idleMaxIntensity = 0.1f;  //Maximum shift amount for the idle loop

    float blackHoleMax = 0.35f; //Maximum shift amount when getting close to a black hole
    float greyScaleMax = 0.75f; //Maximum greyscale amount when getting close to a black hole

    PlayerMovement player; //Player reference to get black hole pull force

    
    void Start()
    {
        player = LevelManager.instance.player.GetComponent<PlayerMovement>();
    }
 
    void FixedUpdate()
    {
        //Idle Shift amount
        float sin = Mathf.Sin(Time.time * idleSpeedModifier);
        float clampedSin = Mathf.Clamp(sin, -0.5f, 0.5f);
        float shiftedSin = clampedSin + 0.5f;
        idleShiftAmount = shiftedSin * idleMaxIntensity;

        //declare effect intensity variables
        float blackHoleShiftAmount = 0f;
        float greyScaleAmount = 0f;

        if (player.distanceToBlackHoles >= 0.3f)
        {
            //Shift amount in black hole range
            float pullRange = Mathf.Clamp(player.blackHolePull.magnitude, 0f, 3f);
            float normal = Mathf.InverseLerp(0f, 3f, pullRange);
            blackHoleShiftAmount = Mathf.Lerp(0f, blackHoleMax, normal);

            //Greyscale amount in black hole range
            greyScaleAmount = Mathf.Lerp(0f, greyScaleMax, normal);
        }

        //Override intensity to max when very close to black hole to prevent effects flashing
        if (player.distanceToBlackHoles < 0.3f)
        {
            blackHoleShiftAmount = blackHoleMax;
            greyScaleAmount = greyScaleMax;
        }

        //applies the higher one of idle shift and black hole shift
        ShaderManager.instance.SetChromaticShift(Mathf.Max(idleShiftAmount, blackHoleShiftAmount));
        ShaderManager.instance.SetGreyscale(greyScaleAmount);
    }
}
