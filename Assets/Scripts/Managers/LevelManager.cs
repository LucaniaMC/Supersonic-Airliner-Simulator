using UnityEngine;
using UnityEngine.SceneManagement;

//Manages dynamic level information and functions, place the prefab in scenes
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;    //Singleton self reference

    public enum LevelStatus { InProgress, Failed, Finished }    //All possible level status
    public LevelStatus status { get; private set; } = LevelStatus.InProgress; //Current status of the level
    public DeathType causeOfDeath { get; private set; } = DeathType.None;  //Death type on fail

    [SerializeField] private LevelDatabase levelDatabase;

    //Current Level Data
    public LevelData currentLevelData { get; private set; }
    public static bool paused = false;  //Is the game paused

    //Player stats on completion
    public int fuelRemaining { get; private set; } = 0;
    public float timeTaken { get; private set; } = 0f;
    public int starRating { get; private set; } = 0;

    float startTime = 0f;   //Logged when the player started flying, used to calculate time taken

    //Threshold for star rating
    public int threeStarThreshold; //fuel needed for 3 stars
    public int twoStarThreshold;   //fuel needed for 2 stars

    //Object References
    public GameObject player { get; private set; }
    public GameObject goal { get; private set; }
    private FuelBar fuelBar;
    private GameObject overlay;
    private Animator overlayAnimator;
    private ScoreMenu scoreMenu;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    //Level setup when new scene loads
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CancelInvoke();

        status = LevelStatus.InProgress;

        // Skip setup if this is the Title Screen
        if (!IsInLevel())
        {
            Debug.Log("LevelManager: Non-level scene loaded. Skipping level setup.");
            return;
        }

        LoadLevelData(scene.name);
        Debug.Log("LevelManager: Loaded " + scene.name);

        goal = GameObject.FindWithTag("Finish");
        player = GameObject.FindWithTag("Player");
        fuelBar = FindObjectOfType<FuelBar>();
        overlay = GameObject.Find("Overlay");
        scoreMenu = FindObjectOfType<ScoreMenu>();
        overlayAnimator = overlay != null ? overlay.GetComponent<Animator>() : null;

        if (currentLevelData != null) AudioManager.instance.PlayMusic(currentLevelData.BGMName);

        causeOfDeath = DeathType.None;
    }


    //Loads corresponding level data from the name of the current scene
    void LoadLevelData(string sceneName)
    {
        currentLevelData = levelDatabase.GetLevelDataFromScene(sceneName);


        if (currentLevelData != null)   //Update variables with database data
        {
            threeStarThreshold = currentLevelData.threeStarThreshold;
            twoStarThreshold = currentLevelData.twoStarThreshold;
        }
        else    //Fallback data if the scene has no data in database
        {
            Debug.LogWarning("LevelManager: No level data found for scene: " + sceneName);
            threeStarThreshold = 50;
            twoStarThreshold = 25;
        }
    }


    //Is the current scene a level
    public bool IsInLevel()
    {
        return SceneManager.GetActiveScene().name.StartsWith("Level ");
    }


    public void Finish()
    {
        status = LevelStatus.Finished;
        Invoke(nameof(ActivateScorePanel), 1.5f);

        //Update level completion stats
        timeTaken = Time.time - startTime;
        fuelRemaining = fuelBar.fuel;
        starRating = CalculateStarRating(fuelRemaining);

        UpdateStars();

        //Unlock next levels
        if (currentLevelData != null) UnlockLevels(currentLevelData.unlockLevels);

        //Save game
        GameManager.instance.SaveProgress();
    }


    //Record when the player starts flying
    public void SetStartTime()
    {
        startTime = Time.time;
    }


    void ActivateScorePanel()
    {
        scoreMenu.Show();
        scoreMenu.Initialize();

        Debug.Log("Fuel: " + fuelRemaining + ", " + "Time: " + timeTaken);
    }


    //Returns star rating stats in the range of 1-3 depending on player's fuel remaining
    int CalculateStarRating(int score)
    {
        if (score >= threeStarThreshold)
        {
            return 3;
        }
        else if (score >= twoStarThreshold)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }


    //Update the highest star information in level data
    void UpdateStars()
    {
        if (currentLevelData == null || GameManager.instance.GetLevelProgress(currentLevelData.sceneNumber) == null)
            return;

        if (starRating > GameManager.instance.GetLevelProgress(currentLevelData.sceneNumber).stars)
        {
            GameManager.instance.SetStars(currentLevelData.sceneNumber, starRating);
        }
    }


    //Unlock the next levels in sequence
    void UnlockLevels(string[] levelNumbers)
    {
        foreach (var levelNumber in levelNumbers)
        {
            GameManager.instance.UnlockLevel(levelNumber);
        }
    }


    public void NextButton()
    { 
        overlay.SetActive(true);
        overlayAnimator.SetBool("Finish", true);
        Invoke("NextScene", 2f);
    }


    public void NextScene()
    {
        //if in test scene, reload the scene
        if (SceneManager.GetActiveScene().name == "Level Test")
        {
            ReloadScene();
            return;
        }

        //if next level doesn't exist, go back to title screen
        if (string.IsNullOrEmpty(currentLevelData.nextLevel))
        {
            Debug.LogWarning("LevelManager: No further scene available. Return to title screen");
            SceneManager.LoadScene("TitleScreen");
        }
        else    //load next level
        {
            SceneManager.LoadScene("Level " + currentLevelData.nextLevel);
        }
    }


    public void Fail(DeathType deathType)
    {
        status = LevelStatus.Failed;
        causeOfDeath = deathType;

        overlay.SetActive(true);
        overlayAnimator.SetBool("OnDeath", true);

        Invoke(nameof(ReloadScene), 2f);
        Debug.Log("LevelManager: Level failed from " + deathType);
    }


    void ReloadScene()
    {
        Scene reloadscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(reloadscene.name);
    }


    public void Resume()
    {
        Time.timeScale = 1f;
        paused = false;
    }


    public void Pause()
    {
        Time.timeScale = 0f;
        paused = true;
    }
}
