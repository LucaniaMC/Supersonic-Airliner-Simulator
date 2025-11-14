using System.Collections;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public float totalTime;
    public float startProgress;
    public float endProgress;

    Coroutine shockwaveCoroutine;
    public SpriteRenderer sprite;
    Material material;
    static int progress = Shader.PropertyToID("_Progress");


    void Start()
    {
        material = sprite.material;
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartShockwave();
        }
    }

    
    public void StartShockwave()
    {
        if (shockwaveCoroutine!= null)
            StopCoroutine(shockwaveCoroutine);

        shockwaveCoroutine = StartCoroutine(ShockwaveAction(startProgress, endProgress));
    }


    IEnumerator ShockwaveAction(float startProgress, float endProgress)
    {
        material.SetFloat(progress, startProgress);

        float lerpedAmount = 0f;
        float elapsedTime = 0f;

        while(elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;

            lerpedAmount = Mathf.Lerp(startProgress, endProgress, elapsedTime / totalTime);
            material.SetFloat(progress, lerpedAmount);

            yield return null;
        }
    }
}
