using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticleEffect : MonoBehaviour
{
    public GameObject player;
    public ParticleSystem windParticle;

    float fadeSpeed = 2f;
    float windSpeedMultiplier = 20f;

    private Coroutine fadeRoutine;

    ParticleSystem.TrailModule trail;
    ParticleSystem.MainModule main;


    void Start()
    {
        //Set particle system references
        trail = windParticle.trails;
        main = windParticle.main;

        //turn off particles on start
        SetVisible(false);
    }


    void Update()
    {
        //follow player
        transform.position = player.transform.position;

        //Test input code
        if (Input.GetKeyDown(KeyCode.A))
        {
            ActivateWind(true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ActivateWind(false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SetSpeed(Random.Range(1f, 3f));
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetRotation(Random.Range(0f, 360f));
        }
    }


    //Fade the particle effect in and out
    public void ActivateWind(bool isActive)
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeTrail(isActive));
    }


    //Set the speed of the particles
    public void SetSpeed(float speed)
    {
        main.startSpeed = speed * windSpeedMultiplier;
    }


    // Set the object's rotation to an angle
    public void SetRotation(float zRotation)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
    }


    //instantly set trail transparency without fading
    public void SetVisible(bool isVisible)
    {
        //stop any fading coroutine
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        if (isVisible) trail.colorOverTrail = new Color(1f, 1f, 1f, 1f);
        else trail.colorOverTrail = new Color(1f, 1f, 1f, 0f);
    }


    //coroutine for fading particle trail color
    IEnumerator FadeTrail(bool fadeIn)
    {
        float startAlpha = trail.colorOverTrail.color.a;
        float targetAlpha = fadeIn ? 1f : 0f;
        float alpha = startAlpha;

        //transition trail color alpha towards target alpha
        while (!Mathf.Approximately(alpha, targetAlpha))
        {
            alpha = Mathf.MoveTowards(alpha, targetAlpha, fadeSpeed * Time.deltaTime);
            trail.colorOverTrail = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        //set the trail color to target color by the end of the loop
        trail.colorOverTrail = new Color(1f, 1f, 1f, targetAlpha);
    }
}
