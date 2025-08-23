using UnityEngine;

// Generic component for applying skins to structures
[ExecuteInEditMode]
public abstract class LoadStructureSkin<TSkin, TEnum> : MonoBehaviour
where TSkin : StructureSkin
{
    public TEnum selectedType;

    public SpriteRenderer mainRenderer;
    public SpriteRenderer shadowRenderer;

    public TSkin[] skins;   //A collection of all skins which can be modified in editor


    protected abstract bool Matches(TSkin skin, TEnum type);


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


    //Match skin based on type
    void ApplySkin()
    {
        //no skins?
        if (skins == null || skins.Length == 0) return;

        foreach (var skin in skins)
        {
            if (Matches(skin, selectedType))
            {
                if (mainRenderer != null) mainRenderer.sprite = skin.Main;
                if (shadowRenderer != null) shadowRenderer.sprite = skin.Shadow;
                return;
            }
        }
    }
}
