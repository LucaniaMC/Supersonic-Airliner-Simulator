using System.Collections.Generic;

[System.Serializable]
public class LevelProgress
{
    public string levelNumber;  // Number of the level
    public bool unlocked;       // Is the level unlocked
    public int stars;   // Highest star rating achieved for this level
}


[System.Serializable]
public class GameData
{
    public List<LevelProgress> levels = new List<LevelProgress>();
}