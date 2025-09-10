using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public string LevelNumber;
    public int starRating = 0;

    [SerializeField] private GameObject starInstance;
    [SerializeField] private GameObject starParent;


    void Start()
    { 
        //Instantiate stars the same amount as the button's star rating
        for (int i = 0; i < starRating; i++)
        {
            Instantiate(starInstance, starParent.transform);
        }
    }
}
