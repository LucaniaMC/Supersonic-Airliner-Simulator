using UnityEngine;
using UnityEngine.UI;

public class SkinTab : MonoBehaviour
{
    public Sprite skinSprite;   //The sprite of the player skin
    public Image targetImage;   //Target image for player skin in each tab
    public Toggle toggle;       //The toggle component
    CustomizeMenu customizeMenu;

    public int skinIndex;   //Which skin does this tab correspond to


    void Start()
    {
        customizeMenu = GetComponentInParent<CustomizeMenu>();
    }


    //Set the skin image in tab to the corresponding skin sprite
    public void UpdateTab()
    {
        targetImage.sprite = skinSprite;
    }


    public void UpdateSelectedSkin()
    {
        customizeMenu.SelectSkin(skinIndex);
    }


    public void PlayClickSound()
    {
        if (toggle.isOn) AudioManager.instance.PlaySFX("Click", true);
    }
}
