using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages game progress, loading and saving game data
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LevelDatabase levelDatabase;
    public GameData gameData = new GameData();


    void Awake()
    {
        //Remove duplicated instances, the first one is kept
        if (instance != null && instance != this)
        {
            Debug.LogWarning("GameManager: duplicate instance destroyed.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadProgress();
    }

    void OnApplicationQuit()
    {
        SaveProgress();
    }


    //Returns the level progress class from a given number
    public LevelProgress GetLevelProgress(string levelNumber)
    {
        return gameData.levels.Find(level => level.levelNumber == levelNumber);
    }


    //Unlocks a given level
    public void UnlockLevel(string levelNumber)
    {
        var level = GetLevelProgress(levelNumber);
        if (level != null) level.unlocked = true;
    }


    //Update stars for a given level
    public void SetStars(string levelNumber, int stars)
    {
        LevelProgress level = GetLevelProgress(levelNumber);
        level.stars = stars;
    }


    //Saves progress to json data in playerprefs
    public void SaveProgress()
    {
        Debug.Log("GameManager: Progress Saved");
        
        string json = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("GameProgress", json);
        PlayerPrefs.Save();
    }


    public void LoadProgress()
    {
        Debug.Log("GameManager: Progress Loaded");

        if (PlayerPrefs.HasKey("GameProgress"))
        {
            //load progress from playerperfs
            string json = PlayerPrefs.GetString("GameProgress");
            gameData = JsonUtility.FromJson<GameData>(json);

            SyncWithDatabase();
        }
        else
        {
            // First time setup, create default progress
            gameData = new GameData();
            foreach (var level in levelDatabase.levels)
            {
                gameData.levels.Add(new LevelProgress
                {
                    levelNumber = level.sceneNumber,
                    unlocked = level.sceneNumber == "1-1", // Only unlock first level
                    stars = 0
                });
            }
            SaveProgress();
        }
    }

    //Ensure progress matches database, add missing levels if database expanded
    void SyncWithDatabase()
    {
        foreach (var level in levelDatabase.levels)
        {
            var progress = GetLevelProgress(level.sceneNumber);
            if (progress == null)
            {
                gameData.levels.Add(new LevelProgress
                {
                    levelNumber = level.sceneNumber,
                    unlocked = false,
                    stars = 0
                });
            }
        }
    }
}
