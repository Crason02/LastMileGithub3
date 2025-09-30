using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    public bool stop = true;
    public Vector3 originalPos;
    private void Awake()
    {
        Instance = this;
        originalPos = transform.localPosition;
    }

    //Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            transform.localPosition = originalPos + new Vector3(x, y, 0);
        }
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }

    public void ShakeOnce(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    public void startShake()
    {
        stop = false;
    }
    public void stopShake()
    {
        stop = true;
    }
}
