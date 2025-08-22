using UnityEngine;


[ExecuteInEditMode]
public class LoadAirportSkin : MonoBehaviour
{
    public AirportType selectedType = AirportType.Black;

    [SerializeField] private SpriteRenderer mainRenderer;
    [SerializeField] private SpriteRenderer shadowRenderer;

    public AirportSkin[] airportSkins;


    //Set the skin at the beginning of the game
    void Start()
    {
        ApplySkin();
    }


    //Preview skin in inspector
    #if UNITY_EDITOR
    void OnValidate()
    {
        if (!Application.isPlaying) ApplySkin();
    }
    #endif


    void ApplySkin()
    {
        //no skins?
        if (airportSkins == null || airportSkins.Length == 0) return;

        foreach (var skin in airportSkins)
        {
            if (skin.type == selectedType)
            {
                if (mainRenderer != null) mainRenderer.sprite = skin.mainSprite;
                if (shadowRenderer != null) shadowRenderer.sprite = skin.shadowSprite;
                return;
            }
        }
    }
}
