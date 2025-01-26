using System.Collections;
using UnityEngine;

public class Pulser : MonoBehaviour
{
    private float elapsedTime = 0f;
    private bool isPulse = false;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (isPulse)
        {
            if (elapsedTime < 0.2f)
            {
                float t = elapsedTime / 0.2f;
                float curvedT = Mathf.SmoothStep(0.9f, 1f, t);

                float scalePulse = Mathf.Lerp(1f, 1.25f, curvedT);
                transform.localScale = originalScale * scalePulse;
            }
            else
            {
                isPulse = false;
                elapsedTime = 0f;
            }
        }
        else
        {
            if (elapsedTime < 0.2f)
            {
                float t = elapsedTime / 0.2f;
                float curvedT = Mathf.SmoothStep(0f, 1f, t);

                float scalePulse = Mathf.Lerp(1.25f, 1f, curvedT);
                transform.localScale = originalScale * scalePulse;
            }
        }
    }

    public void Pulse()
    {
        isPulse = true;
        elapsedTime = 0f;
    }
}
