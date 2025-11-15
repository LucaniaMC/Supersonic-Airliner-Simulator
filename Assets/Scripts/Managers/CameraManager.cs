using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    GameObject cameraRig;
    GameObject mainCamera;
    Coroutine cameraShakeCoroutine;
    public AnimationCurve curve;


    void Awake()
    {
        //Remove duplicated instances, the first one is kept
        if (instance != null && instance != this)
        {
            Debug.LogWarning("CameraManager: Duplicate instance destroyed");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cameraRig = GameObject.FindWithTag("CameraRig");
        mainCamera = GameObject.FindWithTag("MainCamera");
    }


    public void StartCameraShake(float duration, float intensity)
    {
        if(!mainCamera || !cameraRig) return;
        
        if (cameraShakeCoroutine!= null)
            StopCoroutine(cameraShakeCoroutine);

        cameraShakeCoroutine = StartCoroutine(CameraShakeRoutine(duration, intensity));
    }


    IEnumerator CameraShakeRoutine(float duration, float intensity)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            mainCamera.transform.position = (Vector2)cameraRig.transform.position + Random.insideUnitCircle * strength * intensity;
            yield return null;
        }

        mainCamera.transform.position = cameraRig.transform.position;
    }
}
