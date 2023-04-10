using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReporter : MonoBehaviour
{
    private struct JsonStruct { 
        public List<float> list;
    }
    
    [SerializeField] private string fileName = "FPSData";

    [Header("Timings")]
    [SerializeField] private float stepTime = 10f;
    [SerializeField] private int totalSteps = 10;

    [Header("Refs")]
    [SerializeField] private FPSCounter fpsCounter;
    [SerializeField] private ObjectSpawner spawner;

    //vars
    private int currentStep;
    private List<float> findings;

    private void Start()
    {
        findings = new();
        //start test
        StartCoroutine(GatherDataCo());
    }

    //============= Main Test Loop ==============
    private IEnumerator GatherDataCo()
    {
        for (currentStep = 0; currentStep < totalSteps; currentStep++) {
            spawner.SpawnNextStep();
            yield return new WaitForSeconds(stepTime);
            RecordCurrentFPSData();
        }
        OnEndTest(); //close application once testing is done
    }

    // ================= Report Findings ===================
    private void RecordCurrentFPSData()
    {
        findings.Add(fpsCounter.GetCountedFrames() / stepTime);
        ReportDataToJSON();
        //reset counter for next test step
        fpsCounter.ResetCounter();
    }

    private void ReportDataToJSON()
    {
        string jsonStr = JsonUtility.ToJson(new JsonStruct { list = findings }, true);
        //write to external file
        System.IO.File.WriteAllText($"{Application.dataPath}/{fileName}.json", jsonStr);
    }

    // =============== On end final test ===================
    private void OnEndTest()
    {
        Application.Quit();
    }
}
