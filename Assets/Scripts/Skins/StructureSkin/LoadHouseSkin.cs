using UnityEngine;


[ExecuteInEditMode]
public class LoadHouseSkin : MonoBehaviour
{
    public HouseType selectedType = HouseType.Default;

    [SerializeField] private SpriteRenderer mainRenderer;
    [SerializeField] private SpriteRenderer shadowRenderer;

    public HouseSkin[] houseSkins;


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


    //Match skin based on HouseType
    void ApplySkin()
    {
        //no skins?
        if (houseSkins == null || houseSkins.Length == 0) return;

        foreach (var skin in houseSkins)
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
