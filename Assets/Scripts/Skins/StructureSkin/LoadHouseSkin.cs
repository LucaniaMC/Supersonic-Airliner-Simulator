using UnityEngine;


//All house variant names
public enum HouseType { Default, Water, Desert, Snow }


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
        if (houseSkins == null || houseSkins.Length == 0) return;

        if (houseSkins != null)
        {
            foreach (var variant in houseSkins)
        {
            if (variant.type == selectedType)
            {
                if (mainRenderer != null) mainRenderer.sprite = variant.sprite;
                if (shadowRenderer != null) shadowRenderer.sprite = variant.shadowSprite;
                return;
            }
        }
        }
    }
}
