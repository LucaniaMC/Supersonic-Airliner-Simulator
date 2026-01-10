using UnityEngine;
using UnityEngine.UI;

//Changes title menu art to alternate options based on player skin
public class AlternateArt : MonoBehaviour
{
    public Image image;
    public Sprite defaultImage;
    public Sprite alternateImage;


    void Start()
    {
        UpdateArt();
    }


    //Called in Customize Menu event
    public void UpdateArt()
    {
        if(PlayerPrefs.HasKey("skin"))
        {
            int skinIndex = PlayerPrefs.GetInt("skin");

            if (skinIndex == 1)
            {
                image.sprite = alternateImage;
            }
            else
            {
                image.sprite = defaultImage;
            }
        }
    }
}
