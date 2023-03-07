using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private float timeToMeasure = 1f;
    private int frames = 0;

    private void Start()
    {
        StartCoroutine(MeasureFPSCo());
    }

    private void Update()
    {
        frames++;
    }

    private IEnumerator MeasureFPSCo()
    {
        yield return new WaitForSeconds(timeToMeasure);
        ReportResult();
        ResetCounter();
        StartCoroutine(MeasureFPSCo());
    }
    private void ReportResult()
    {
        float fps = frames / timeToMeasure;
        Debug.Log(fps + " fps these " + timeToMeasure + " seconds");
    }
    private void ResetCounter()
    {
        frames = 0;
    }
}
