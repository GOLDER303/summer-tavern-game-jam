using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focusable : MonoBehaviour
{
    [SerializeField] public float pulseSpeed = 0.002f;
    [SerializeField] public float maxScale = .4f;
    [SerializeField] public float minScale = .3f;

    private float currentScale = 1f;
    private bool isPulsing;
    private Coroutine pulseCoroutine;
    private Vector3 baseTransformScale;

    private void Awake()
    {
        baseTransformScale = transform.localScale;
    }

    private IEnumerator PulseCoroutine()
    {
        isPulsing = true;
        while (true)
        {
            while (currentScale != maxScale)
            {
                currentScale = Mathf.MoveTowards(currentScale, maxScale, pulseSpeed);

                transform.localScale = Vector2.one * currentScale;

                yield return new WaitForEndOfFrame();
            }

            while (currentScale != minScale)
            {
                currentScale = Mathf.MoveTowards(currentScale, minScale, pulseSpeed);

                transform.localScale = Vector2.one * currentScale;

                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void Focus()
    {
        if (!isPulsing)
        {
            pulseCoroutine = StartCoroutine(PulseCoroutine());
        }
    }

    public void UnFocus()
    {
        if (isPulsing)
        {
            StopCoroutine(pulseCoroutine);
            transform.localScale = baseTransformScale;
            isPulsing = false;
        }
    }
}
