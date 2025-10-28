using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticleEffect : MonoBehaviour
{
    public GameObject player;
    public ParticleSystem windParticle;

    float fadeSpeed = 2f;               //Speed for the particle fade in and out
    float windSpeedMultiplier = 20f;    //Speed multiplier for particles

    private Coroutine fadeRoutine;  //Coroutine reference for fading

    //Particle system references
    ParticleSystem.TrailModule trail;
    ParticleSystem.MainModule main;
    ParticleSystem.VelocityOverLifetimeModule velocity;


    void Start()
    {
        //Set particle system references
        trail = windParticle.trails;
        main = windParticle.main;
        velocity = windParticle.velocityOverLifetime;

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
            float randomSpeed = Random.Range(0.5f, 3f);
            Debug.Log("Set speed to: " + randomSpeed);
            SetSpeed(randomSpeed);
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            float randomRotation = Random.Range(0f, 360f);
            Debug.Log("Set rotation to: " + randomRotation);
            SetRotation(randomRotation);
        }
    }


    //Fade the particle effect in and out
    public void ActivateWind(bool isActive)
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeTrail(isActive));
    }


    //Set the speed of the particles with velocity over time
    public void SetSpeed(float speed)
    {
        velocity.z = speed * windSpeedMultiplier;
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

        //set alpha to 0 for invisible and 1 for visible 
        if (isVisible) trail.colorOverTrail = new Color(1f, 1f, 1f, 1f);
        else trail.colorOverTrail = new Color(1f, 1f, 1f, 0f);
    }


    //coroutine for fading particle trail color
    IEnumerator FadeTrail(bool fadeIn)
    {
        float startAlpha = trail.colorOverTrail.color.a;    //Start with current alpha to prevent jumps
        float targetAlpha = fadeIn ? 1f : 0f;   //fade alpha to 1 if fadein, to 0 if fadeout
        float alpha = startAlpha;   //current alpha

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
