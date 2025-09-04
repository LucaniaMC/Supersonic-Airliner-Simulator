using UnityEngine;
using UnityEngine.UI;

public class SkinTab : MonoBehaviour
{
    public Sprite skinSprite;
    public Image targetImage;

    public int skinIndex;


    public void UpdateTab()
    {
        targetImage.sprite = skinSprite;
    }


    public void UpdateSkin()
    {

    }
}
