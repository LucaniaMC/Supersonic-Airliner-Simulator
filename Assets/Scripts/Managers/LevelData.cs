using UnityEngine;

public enum LevelWorld { Day, Night, Ocean, Desert, Snow, Space }

[System.Serializable]
public class LevelData
{
    [Header("Level Info")]
    public string sceneNumber;  //Numbers only, such as 1-1, 1-2, 2-3
    public LevelWorld world;    //The world type of the level
    public bool isNight;        //Is the level a nighttime level
    public string BGMName;      //Name of the background music for the level

    [Header("Score Threshold")]
    public int threeStarThreshold = 50; //fuel needed for 3 stars
    public int twoStarThreshold = 25;   //fuel needed for 2 stars

    [Header("Level Stats")]
    public bool unlocked = true;    //Is this level unlocked and playable
    [Range (0, 3)] public int starRating = 0;   //Highest star rating obtained for this level
}
