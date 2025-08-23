using UnityEngine;
using UnityEngine.UI;

public class PlayerSkinManager : MonoBehaviour
{
    public PlayerSkinDatabase playerSkinDatabase;

    public Image artworkSprite;

    private int selectedOption = 0;

    void Start()
    {
        if(!PlayerPrefs.HasKey("skin")) //Check if there is skin saved
        {
            selectedOption = 0;
        } 
        else
        {
            LoadSkin();
        }

        UpdateSkin(selectedOption);
    }


    public void NextOption() //switch to next skin
    {
        selectedOption++;

        if (selectedOption >= playerSkinDatabase.SkinCount)
        {
            selectedOption = 0;
        }

        UpdateSkin(selectedOption);
        FindObjectOfType<AudioManager>().PlaySFX("Click", true);
    }


    public void BackOption() //switch to previous skin
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = playerSkinDatabase.SkinCount - 1;
        }

        UpdateSkin(selectedOption);
        FindObjectOfType<AudioManager>().Play("Click");
    }


    private void UpdateSkin(int selectedOption)
    {
        PlayerSkin playerSkin = playerSkinDatabase.GetSkin(selectedOption);
        artworkSprite.sprite = playerSkin.playerSprite;
    }
    
    
    public void LoadSkin()
    {
        selectedOption = PlayerPrefs.GetInt("skin");
    }


    public void SaveSkin()
    {
        PlayerPrefs.SetInt("skin", selectedOption);
    }
}
