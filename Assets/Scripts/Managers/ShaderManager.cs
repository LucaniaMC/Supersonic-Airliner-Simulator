using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShaderManager : MonoBehaviour
{
    public static ShaderManager instance; //Singleton self reference

    [SerializeField] Renderer2DData mainRenderer; //Renderer for the main camera
    private List<Material> rendererMaterials = new List<Material>(); //All fullscreen render feature materials in the main renderer
        
    void Awake()
    {
        //Remove duplicated instances, the first one is kept
        if (instance != null && instance != this)
        {
            Debug.LogWarning("ShaderManager: duplicate instance destroyed.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        
        //Get material reference for all rendering features
        foreach (var feature in mainRenderer.rendererFeatures)
        {
            if (feature is FullScreenPassRendererFeature fullScreen)
            {
                rendererMaterials.Add(fullScreen.passMaterial);
            }
        }
    }

    #region Post Effects
    public void SetChromaticShift(float amount)
    {
        rendererMaterials[2].SetFloat("_ShiftAmount", amount);
    }

    public void SetGreyscale(float amount)
    {
        rendererMaterials[1].SetFloat("_Greyscale", amount);
    }

    public void SetGlow(float amount)
    {
        rendererMaterials[0].SetFloat("_Intensity", amount);
    }

    //Reset material values on quit to prevent runtime changes from being saved
    void OnApplicationQuit()
    {
        rendererMaterials[2].SetFloat("_ShiftAmount", 0f);
        rendererMaterials[1].SetFloat("_Greyscale", 0f);
        rendererMaterials[0].SetFloat("_Intensity", 0f);
    }
    #endregion
}
