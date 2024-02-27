using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    public float duration;  // time
    public float magnitude; // sheddat

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void shake()
    {
        StartCoroutine(Shaker());
    }

    IEnumerator Shaker()
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0;

        while (elapsed < duration)
        {

            float x = Random.Range(-1f, 1) * magnitude;
            float y = Random.Range(-1f, 1) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
