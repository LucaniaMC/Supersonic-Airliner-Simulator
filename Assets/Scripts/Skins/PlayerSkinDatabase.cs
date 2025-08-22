using UnityEngine;

[CreateAssetMenu]
public class PlayerSkinDatabase : ScriptableObject
{
    public PlayerSkin[] playerSkin;

    public int SkinCount 
    {
        get 
        {
            return playerSkin.Length;
        }
    }

    public PlayerSkin GetSkin(int index) 
    {
        return playerSkin[index];
    }
}
