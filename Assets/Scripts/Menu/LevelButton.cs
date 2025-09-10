using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public LevelDatabase levelDatabase;
    public Button button;
    public TextMeshProUGUI text;

    public string levelNumber;

    [SerializeField] private GameObject starInstance;
    [SerializeField] private GameObject starParent;


    void Start()
    {
        LoadLevelData(levelNumber);
    }


    void LoadLevelData(string levelNumber)
    {
        LevelData data = levelDatabase.GetLevelData(levelNumber);

        if (data == null)
        {
            Debug.Log("LevelButton: No level data available for " + levelNumber);
            return;
        }

        if (data.unlocked == true)
        {
            button.interactable = true;
            DisplayStars(data.starRating);
        }
        else
        {
            button.interactable = false;
            text.enabled = false;
        }
    }


    void DisplayStars(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(starInstance, starParent.transform);
        }
    }
}
