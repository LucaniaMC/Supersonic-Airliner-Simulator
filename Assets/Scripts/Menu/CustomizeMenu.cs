using UnityEngine;
using UnityEngine.UI;

public class CustomizeMenu : UIMenu
{
    public PlayerSkinDatabase playerSkinDatabase;

    public Image skinSprite;
    public Image artSprite;

    public GameObject skinTab;
    public GameObject skinTabGroup;

    private int tabGroupSize = 12;

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


        for (int i = 0; i < tabGroupSize; i++)
        {
            PlayerSkin skins = playerSkinDatabase.playerSkin[i];
            GameObject tab = Instantiate(skinTab, skinTabGroup.transform);
            SkinTab tabInstance = tab.GetComponent<SkinTab>();
            tabInstance.skinSprite = skins.playerSprite;
            tabInstance.UpdateTab();
            tabInstance.skinIndex = i; // index of current iteration
        }
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
        //skinSprite.sprite = playerSkin.playerSprite;
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
