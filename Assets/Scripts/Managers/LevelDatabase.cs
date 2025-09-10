using UnityEngine;

[CreateAssetMenu]
public class LevelDatabase : ScriptableObject
{
    public LevelData[] levels;

    public LevelData GetLevelData(string sceneName)
    {
        // Remove "Level " prefix from scene name
        string shortName = sceneName.Replace("Level ", "");

        foreach (var level in levels)
        {
            if (level.sceneNumber == shortName)
                return level;
        }
        return null;
    }


    public void UnlockLevel(string levelName)
    {

    }


    public void SetStars(string levelName, int amount)
    { 
        
    }
}