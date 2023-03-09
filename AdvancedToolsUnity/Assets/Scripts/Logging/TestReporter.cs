using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReporter : MonoBehaviour
{
    [System.Serializable]
    private class TestData {
        public List<FPSDataSet> fpsDataSets;

        public TestData() { fpsDataSets = new List<FPSDataSet>(); }
    }

    [System.Serializable]
    public class FPSDataSet {
        public List<float> fpsSets;
        public float averageFPS;
        public int objectCount;

        public FPSDataSet(int objectCount) { 
            fpsSets = new List<float>();
            this.objectCount = objectCount;
        }

        public void CalculateAverageFPS()
        {
            float totalFPS = 0f;
            foreach (float fps in fpsSets) {
                totalFPS += fps;
            }
            averageFPS = totalFPS / fpsSets.Count;
        }
    }

    [SerializeField] private float stepTime = 10f;
    [SerializeField] private float totalTestTime = 60f;
    [SerializeField] private string fileName;

    [Header("Refs")]
    [SerializeField] private FPSCounter fpsCounter;
    [SerializeField] private ObjectSpawner spawner;

    private TestData testData;

    private void Awake()
    {
        //setup data
        testData = new TestData();
        //setup listeners
        fpsCounter.onReportFPS += AddFPSDataPoint;
        //run test
        StartCoroutine(RunTestCo());
    }

    private void Start()
    {
        SetupNextTestStep();
    }

    //================ handle step increments =====================
    private IEnumerator IncrementStepCo()
    {
        yield return new WaitForSeconds(stepTime);
        spawner.NextStep();
        SetupNextTestStep();
    }

    private void SetupNextTestStep()
    {
        testData.fpsDataSets.Add(new FPSDataSet(spawner.totalCount));
    }

    //================ handle data collection =====================
    private void AddFPSDataPoint(float fps)
    {
        testData.fpsDataSets[^1].fpsSets.Add(fps); // ^1 == count - 1
        testData.fpsDataSets[^1].CalculateAverageFPS();
    }

    //==================== report results ==============================
    private IEnumerator RunTestCo()
    {
        for (int i = 0; i < Mathf.FloorToInt(totalTestTime / stepTime) - 1; i++) {
            yield return IncrementStepCo();
        }
        yield return new WaitForSeconds(stepTime); //wait for final step to finish
        WriteReportFile();
    }

    private void WriteReportFile()
    {
        //create JSON
        string dataString = JsonUtility.ToJson(testData, true);
        //create new file
        System.IO.File.WriteAllText(Application.dataPath + "/" + fileName + ".json", dataString);
    }
}
