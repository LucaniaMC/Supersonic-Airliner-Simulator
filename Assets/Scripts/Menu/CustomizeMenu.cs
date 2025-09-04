using UnityEngine;
using UnityEngine.UI;

public class CustomizeMenu : UIMenu
{
    public PlayerSkinDatabase playerSkinDatabase;

    public Image artworkSprite;
    public Image artSprite;

    private int selectedOption = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("skin")) //Check if there is skin saved
        {
            selectedOption = 0;
        }
        else
        {
            LoadSkin();
        }

        UpdateSkin(selectedOption);

        //load skin menu
    }


    public void SelectSkin(int option)
    {
        selectedOption = option;
        UpdateSkin(selectedOption);
    }


    private void UpdateSkin(int selectedOption)
    {
        PlayerSkin playerSkin = playerSkinDatabase.GetSkin(selectedOption);
        artSprite.sprite = playerSkin.artSprite;
        //artworkSprite.sprite = playerSkin.playerSprite;
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
