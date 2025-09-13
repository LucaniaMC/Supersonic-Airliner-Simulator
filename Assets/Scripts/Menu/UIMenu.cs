using UnityEngine;

public abstract class UIMenu : MonoBehaviour
{
    [SerializeField] protected GameObject window; //The window child object that gets enabled and disabled


    public virtual void Show()
    {
        window.SetActive(true);
    }


    public virtual void Hide()
    {
        window.SetActive(false);
    }


    public void PlayClickSound()
    {
        AudioManager.instance.PlaySFX("Click", true);
    }
}
