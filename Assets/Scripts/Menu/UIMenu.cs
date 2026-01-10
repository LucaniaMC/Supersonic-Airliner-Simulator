using UnityEngine;
using UnityEngine.Events;

public abstract class UIMenu : MonoBehaviour
{
    [SerializeField] protected GameObject window; //The window child object that gets enabled and disabled

    [Header("Menu Events")]
    public UnityEvent OnShow;
    public UnityEvent OnHide;


    public virtual void Show()
    {
        window.SetActive(true);
        OnShow?.Invoke();
    }


    public virtual void Hide()
    {
        window.SetActive(false);
        OnHide?.Invoke();
    }


    public void PlayClickSound()
    {
        AudioManager.instance.PlaySFX("Click", true);
    }
}
