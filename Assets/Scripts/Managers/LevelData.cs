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

    [Header("Level Sequence")]
    public string[] unlockLevels; //Number of levels that will be unlocked when this level is completed
    public string nextLevel;    //Number of level that will be played next in sequence
}
