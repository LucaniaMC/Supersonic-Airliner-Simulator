using UnityEngine;
using UnityEngine.UI;

public class SkinTab : MonoBehaviour
{
    public Sprite skinSprite;   //The sprite of the player skin
    public Image targetImage;   //Target image for player skin in each tab
    public Toggle toggle;       //The toggle component
    public CustomizeMenu customizeMenu; //Reference back to the menu, set in the CustomizeMenu script

    public int skinIndex;   //Which skin does this tab correspond to


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


    //force toggle to stay on when clicked without the toogle group setting so it's possible for no tabs to be selected
    public void ToggleOn()
    {
        toggle.SetIsOnWithoutNotify(true);
    }
}
