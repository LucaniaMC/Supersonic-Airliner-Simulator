using UnityEngine;
using UnityEngine.SceneManagement;

//Manages dynamic level information, place the prefab in scenes
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;    //Singleton self reference

    public enum LevelStatus { InProgress, Failed, Finished }    //All possible level status
    public LevelStatus status = LevelStatus.InProgress; //Current status of the level

    [SerializeField] private LevelDatabase levelDatabase;

    //Current Level Data
    public LevelWorld world = LevelWorld.Day; //What world is this level in
    public bool isNight = false; //Is the level a night level
    public string BGMName;  //BGM that plays in the level

    public static bool paused = false;  //Is the game paused

    //Player stats on completion
    public int fuelRemaining { get; private set; } = 0;
    public float timeTaken { get; private set; } = 0f;

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
        status = LevelStatus.InProgress;

        // Skip setup if this is the Title Screen
        if (scene.name == "TitleScreen")
        {
            Debug.Log("LevelManager: Non-level scene loaded. Skipping level setup.");
            return;
        }

        LoadLevelData("Level " + scene.name);

        goal = GameObject.FindWithTag("Finish");
        player = GameObject.FindWithTag("Player");
        fuelBar = FindObjectOfType<FuelBar>();
        overlay = GameObject.Find("Overlay");
        scoreMenu = FindObjectOfType<ScoreMenu>();
        overlayAnimator = overlay != null ? overlay.GetComponent<Animator>() : null;

        AudioManager.instance.PlayMusic(BGMName);
    }


    //Update level information according to corresponding data 
    void LoadLevelData(string sceneName)
    {
        LevelData data = levelDatabase.GetLevelData(sceneName);


        if (data != null)   //Update variables with database data
        {
            world = data.world;
            isNight = data.isNight;
            BGMName = data.BGMName;

            threeStarThreshold = data.threeStarThreshold;
            twoStarThreshold = data.twoStarThreshold;
        }
        else    //Fallback data if the scene has no data in database
        {
            Debug.LogWarning("LevelManager: No level data found for scene: " + sceneName);
            world = LevelWorld.Day;
            isNight = false;
            BGMName = "";

            threeStarThreshold = 50;
            twoStarThreshold = 25;
        }
    }


    public bool IsInLevel()
    {
        return SceneManager.GetActiveScene().name != "TitleScreen";
    }


    public void Finish()
    {
        overlay.SetActive(true);

        //overlayAnimator.SetBool("Finish", true);
        Invoke("ActivateScorePanel", 1.5f);
        //Invoke("NextScene", 2f);
    }


    //called when the player starts flying
    public void SetStartTime()
    {
        startTime = Time.time;
    }


    void ActivateScorePanel()
    {
        scoreMenu.Show();
        timeTaken = Time.time - startTime;
        fuelRemaining = fuelBar.fuel;
        scoreMenu.Initialize();

        Debug.Log("Fuel: " + fuelRemaining + ", " + "Time: " + timeTaken);
    }


    public void NextScene()
    {
        //if in test scene, reload the scene
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            Invoke("ReloadScene", 0f);
            return;
        }
        //if next scene index doesn't exist, go back to title screen
        if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCountInBuildSettings - 1)
        {
            Debug.LogWarning("LevelManager: No further scene available. Return to title screen");
            SceneManager.LoadScene("TitleScreen");
        }
        else    //load next scene
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    public void Fail()
    {
        overlay.SetActive(true);
        overlayAnimator.SetBool("OnDeath", true);

        Invoke("ReloadScene", 2f);
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
