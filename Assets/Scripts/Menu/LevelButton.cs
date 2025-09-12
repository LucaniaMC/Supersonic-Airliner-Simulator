using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
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
        LevelProgress progress = GameManager.instance.GetLevelProgress(levelNumber);

        if (progress == null)
        {
            Debug.Log("LevelButton: No level data available for " + levelNumber);
            return;
        }

        if (progress.unlocked == true)
        {
            button.interactable = true;
            DisplayStars(progress.stars);
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
