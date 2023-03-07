using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReporter : MonoBehaviour
{
    [System.Serializable]
    private class TestData {
        public List<FPSDataSet> fpsDataSets;
        public float averageFPS;

        public TestData() { fpsDataSets = new List<FPSDataSet>(); }
    }

    [System.Serializable]
    public class FPSDataSet {
        public List<float> fpsSets;
        public float averageFPS { get { return CalculateAverageFPS(); } }

        public FPSDataSet() { fpsSets = new List<float>(); }

        public float CalculateAverageFPS()
        {
            float totalFPS = 0f;
            foreach (float fps in fpsSets) {
                totalFPS += fps;
            }
            return totalFPS / fpsSets.Count;
        }
    }

    [SerializeField] private float totalTestTime = 60f;
    [SerializeField] private string fileName;

    [SerializeField] private FPSCounter fpsCounter;

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

    //================ handle data collection =====================
    private void AddFPSDataPoint(float fps)
    {
        testData.fpsDataSets[testData.fpsDataSets.Count - 1].fpsSets.Add(fps);
    }

    //==================== report results ==============================
    private IEnumerator RunTestCo()
    {
        yield return new WaitForSeconds(totalTestTime);
        WriteReportFile();
    }

    private void WriteReportFile()
    {
        //create JSON
        string dataString = JsonUtility.ToJson(testData);
        //create new file
        System.IO.File.WriteAllText(Application.dataPath + "/" + fileName + ".json", dataString);
    }
}
