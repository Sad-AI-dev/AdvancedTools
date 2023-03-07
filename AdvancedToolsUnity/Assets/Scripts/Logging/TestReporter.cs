using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReporter : MonoBehaviour
{
    [System.Serializable]
    private struct TestData {
        [System.Serializable]
        public struct FPSData {
            public float fps;
        }

        public List<FPSData> fpsData;
        public float averageFPS;

        public void CalculateAverageFPS()
        {
            float totalFPS = 0f;
            foreach (FPSData data in fpsData) {
                totalFPS += data.fps;
            }
            averageFPS = totalFPS / fpsData.Count;
        }
    }

    [SerializeField] private float totalTestTime = 60f;
    [SerializeField] private string fileName;

    [SerializeField] private FPSCounter fpsCounter;

    private TestData testData;

    private void Start()
    {
        //setup data
        testData = new TestData();
        testData.fpsData = new List<TestData.FPSData>();
        //setup listeners
        fpsCounter.onReportFPS += AddFPSDataPoint;
        //run test
        StartCoroutine(RunTestCo());
    }

    //================ handle data collection =====================
    private void AddFPSDataPoint(float fps)
    {
        testData.fpsData.Add(new TestData.FPSData { fps = fps });
    }

    //==================== report results ==============================
    private IEnumerator RunTestCo()
    {
        yield return new WaitForSeconds(totalTestTime);
        WriteReportFile();
    }

    private void WriteReportFile()
    {
        //calc avg fps
        testData.CalculateAverageFPS();
        //create JSON
        string dataString = JsonUtility.ToJson(testData);
        //create new file
        System.IO.File.WriteAllText(Application.dataPath + "/" + fileName + ".json", dataString);
    }
}
