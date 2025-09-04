using UnityEngine;
using UnityEngine.UI;

public class SkinTab : MonoBehaviour
{
    public Sprite skinSprite;
    public Image targetImage;
    CustomizeMenu customizeMenu;

    public int skinIndex;


    public void UpdateTab()
    {
        targetImage.sprite = skinSprite;
        customizeMenu = GetComponentInParent<CustomizeMenu>();
    }


    public void UpdateSelectedSkin()
    {
        customizeMenu.SelectSkin(skinIndex);
    }
}
