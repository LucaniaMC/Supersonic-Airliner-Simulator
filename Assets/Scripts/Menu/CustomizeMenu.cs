using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeMenu : UIMenu
{
    public PlayerSkinDatabase playerSkinDatabase;

    public Image artSprite;     //Target image for skin splash arts
    public TextMeshProUGUI description;
    public GameObject skinTab;  //Prefab for the skin tab
    public GameObject skinTabGroup; //The parent game object to summon skin tab prefabs under
    public ToggleGroup toggleGroup; //The toggle group for all tabs

    [SerializeField] private int selectedOption = 0; //The skin currectly selected by the player

    [SerializeField] private int currentPage = 0;
    [SerializeField] private int tabsPerPage = 12;  //maximum number of tabs per page


    void Start()
    {
        //Check if there is skin saved
        if (!PlayerPrefs.HasKey("skin"))
        {
            selectedOption = 0;
        }
        else
        {
            LoadSkin();
        }

        //display currently selected skin
        UpdateSkin(selectedOption);

        //shows initial page
        ShowPage(currentPage);
    }


    public void ShowPage(int pageIndex)
    {
        // Calculate the number of pages available
        int maxPage = Mathf.CeilToInt((float)playerSkinDatabase.SkinCount / tabsPerPage) - 1;
        // Clamp the current page index so it doesn't go past available pages
        currentPage = Mathf.Clamp(pageIndex, 0, maxPage);

        // If page is invalid, do nothing
        if (pageIndex < 0 || pageIndex > maxPage)
            return;

        Debug.Log("Customize Menu: New Page Loaded");

        //Destroy old instances
        foreach (Transform child in skinTabGroup.transform)
        {
            Destroy(child.gameObject);
        }

        // Calculate start and end indexes for this page
        int startIndex = currentPage * tabsPerPage;
        int endIndex = Mathf.Min(startIndex + tabsPerPage, playerSkinDatabase.SkinCount);

        // Spawn instances for the skins for the current page
        for (int i = startIndex; i < endIndex; i++)
        {
            //Instantiate the object
            PlayerSkin skins = playerSkinDatabase.playerSkin[i];
            GameObject tab = Instantiate(skinTab, skinTabGroup.transform);

            //Update tab appearance
            SkinTab tabInstance = tab.GetComponent<SkinTab>();
            tabInstance.skinSprite = skins.playerSprite;
            tabInstance.UpdateTab();

            //Update tab index
            tabInstance.skinIndex = i;
            
            // Add it to the ToggleGroup
            tabInstance.toggle.group = toggleGroup;

            // Sync selection
            tabInstance.toggle.SetIsOnWithoutNotify(i == selectedOption);
        }
    }


    //Update current skin display to a given index
    private void UpdateSkin(int selectedOption)
    {
        PlayerSkin playerSkin = playerSkinDatabase.GetSkin(selectedOption);
        artSprite.sprite = playerSkin.artSprite;
        description.text = playerSkin.description;
    }


    #region Button Functions
    public void NextPage()
    {
        ShowPage(currentPage + 1);
    }


    public void PreviousPage()
    {
        ShowPage(currentPage - 1);
    }


    public void SelectSkin(int option)
    {
        selectedOption = option;
        UpdateSkin(selectedOption);
    }


    public void LoadSkin()
    {
        selectedOption = PlayerPrefs.GetInt("skin");
    }


    public void SaveSkin()
    {
        PlayerPrefs.SetInt("skin", selectedOption);
    }
    #endregion
}
