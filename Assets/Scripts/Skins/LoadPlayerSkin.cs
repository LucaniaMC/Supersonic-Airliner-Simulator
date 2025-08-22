using UnityEngine;

public class LoadPlayerSkin : MonoBehaviour
{
    public SpriteRenderer artworkSprite; //player skin change
    public PlayerSkinDatabase playerSkinDatabase;
    private int selectedOption = 0;
    

    void Start() //same code as PlayerSkinManager, load player skin
    {
        artworkSprite = GameObject.Find("PlaneSprite").GetComponent<SpriteRenderer>();

        if(!PlayerPrefs.HasKey("skin")) 
        {
            selectedOption = 0;
        } 
        else
        {
            LoadSkin();
        }

        UpdateSkin(selectedOption);
    }

    private void UpdateSkin (int selectedOption) 
    {
        PlayerSkin playerSkin = playerSkinDatabase.GetSkin(selectedOption);
        artworkSprite.sprite = playerSkin.playerSprite;
    }
    
    private void LoadSkin() 
    {
        selectedOption = PlayerPrefs.GetInt("skin");
    }

}
