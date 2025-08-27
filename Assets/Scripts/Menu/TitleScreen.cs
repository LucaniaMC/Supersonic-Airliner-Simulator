using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject overlay;

    [SerializeField] TextMeshProUGUI versionNumberText;

    void Start()
    {
        Invoke("DisableOverlay", 1f);
        versionNumberText.text = "V " + Application.version;
        AudioManager.instance.PlayMusic("MenuMusic");
    }


    //turns off overlay to prevent it from blocking clicks
    void DisableOverlay()
    {
        overlay.SetActive(false);
    }
}
