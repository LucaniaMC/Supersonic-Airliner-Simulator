using UnityEngine;

public class StopParticleAfterTime : MonoBehaviour
{
    //time after which the particle stops
    public float time = 0f;
    ParticleSystem particles;


    void Start()
    {
        particles = FindObjectOfType<ParticleSystem>();
        Invoke(nameof(StopParticle), time);
    }


    void StopParticle()
    {
        ParticleSystem.EmissionModule emission = particles.emission;
        emission.enabled = false;
    }
}
