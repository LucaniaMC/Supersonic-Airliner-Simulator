using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShaderManager : MonoBehaviour
{
    public static ShaderManager instance;

    [SerializeField] Renderer2DData mainRenderer;

    public Dictionary<string, Material> rendererMaterials = new Dictionary<string, Material>();

    void Awake()
    {
        // Remove duplicated instances
        if (instance != null && instance != this)
        {
            Debug.LogWarning("ShaderManager: duplicate instance destroyed.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Find all fullscreen render features
        foreach (var feature in mainRenderer.rendererFeatures)
        {
            if (feature is FullScreenPassRendererFeature fullScreen)
            {
                Material material = fullScreen.passMaterial;

                if (material == null)
                    continue;

                string shaderName = feature.name;
                print(feature.name);

                rendererMaterials[shaderName] = material;
            }
        }
    }

    //Get material reference by shader name
    private Material GetMaterial(string name)
    {
        if (rendererMaterials.TryGetValue(name, out Material material))
            return material;

        Debug.LogWarning($"ShaderManager: Could not find shader '{name}'.");
        return null;
    }


    #region Value Controls
    public void SetChromaticShift(float amount)
    {
        GetMaterial("ChromaticShift").SetFloat("_ShiftAmount", amount);
    }

    public void SetGreyscale(float amount)
    {
        GetMaterial("Greyscale").SetFloat("_Greyscale", amount);
    }

    public void SetGlow(float amount)
    {
        GetMaterial("Glow").SetFloat("_Intensity", amount);
    }
    #endregion


    void OnApplicationQuit()
    {
        //Reset value to 0 to prevent runtime change being saved
        GetMaterial("ChromaticShift")?.SetFloat("_ShiftAmount", 0f);
        GetMaterial("Greyscale")?.SetFloat("_Greyscale", 0f);
        GetMaterial("Glow")?.SetFloat("_Intensity", 0f);
    }
}