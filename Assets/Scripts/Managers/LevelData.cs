
public enum LevelWorld { Day, Night, Ocean, Desert, Snow, Space }

[System.Serializable]
public class LevelData
{
    public string sceneNumber; //Numbers only, such as 1-1, 1-2, 2-3
    public LevelWorld world;
    public bool isNight;
    public string BGMName;
}
