using UnityEngine;

[CreateAssetMenu]
public class LevelDatabase : ScriptableObject
{
    public LevelData[] levels;


    //Returns a level data from scene name
    public LevelData GetLevelDataFromScene(string sceneName)
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

    //Returns a level data from level number
    public LevelData GetLevelData(string levelNumber)
    {
        foreach (var level in levels)
        {
            if (level.sceneNumber == levelNumber)
                return level;
        }
        return null;
    }
}