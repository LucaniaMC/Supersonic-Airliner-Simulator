using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public GameObject cameraRig {get; private set;}   //Secondary camera used for screen position calculations unaffected by screen shake
    public GameObject mainCamera {get; private set;}  //Main camera used for rendering

    Coroutine cameraShakeCoroutine;
    Coroutine zoomCoroutine;

    public AnimationCurve shakeCurve;


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
        //Get references
        cameraRig = GameObject.FindWithTag("CameraRig");
        mainCamera = GameObject.FindWithTag("MainCamera");

        //prevent coroutine from carrying over between scenes
        StopAllCoroutines();
    }


    public void Zoom(float targetScale, float speed)
    {
        if (!mainCamera) return;

        // Stop any existing zoom coroutine
        if (zoomCoroutine != null)
            StopCoroutine(zoomCoroutine);

        zoomCoroutine = StartCoroutine(ZoomRoutine(targetScale, speed));
    }


    public void ZoomOverTime(float targetScale, float time)
    {
        if (!mainCamera) return;

        // Kill previous zoom if one is running
        if (zoomCoroutine != null)
            StopCoroutine(zoomCoroutine);

        zoomCoroutine = StartCoroutine(ZoomOverTimeRoutine(targetScale, time));
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
            elapsedTime += Time.fixedDeltaTime;
            float strength = shakeCurve.Evaluate(elapsedTime / duration);
            mainCamera.transform.position = (Vector2)cameraRig.transform.position + Random.insideUnitCircle * strength * intensity;
            yield return new WaitForFixedUpdate();
        }

        mainCamera.transform.position = cameraRig.transform.position;
    }


    IEnumerator ZoomRoutine(float targetScale, float speed)
    {
        Camera cam = mainCamera.GetComponent<Camera>();
        if (cam == null) yield break;

        // Always move in the correct direction
        while (!Mathf.Approximately(cam.orthographicSize, targetScale))
        {
            cam.orthographicSize = Mathf.MoveTowards(
                cam.orthographicSize,
                targetScale,
                speed * Time.deltaTime
            );

            yield return null;
        }
    }


    IEnumerator ZoomOverTimeRoutine(float targetScale, float duration)
    {
        Camera cam = mainCamera.GetComponent<Camera>();
        if (cam == null) yield break;

        float startSize = cam.orthographicSize;
        float endSize = targetScale;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            cam.orthographicSize = Mathf.Lerp(startSize, endSize, t);

            yield return null;
        }

        cam.orthographicSize = endSize;
    }
}
