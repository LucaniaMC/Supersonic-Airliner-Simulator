using UnityEngine;
using UnityEngine.UI;


//Gives sliders a click sound, hover and hold textures
public class SliderHandleClick : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite holdSprite;

    void Start()
    {
        targetImage.sprite = defaultSprite;
    }


    public void SliderHover()
    {
        targetImage.sprite = holdSprite;
    }


    public void SliderDown()
    {
        targetImage.sprite = holdSprite;
    }


    public void SliderUp()
    {
        targetImage.sprite = defaultSprite;
        AudioManager.instance.PlaySFX("Click", true);
    }
    

    public void SliderReset()
    {
        targetImage.sprite = defaultSprite;
    }
}
