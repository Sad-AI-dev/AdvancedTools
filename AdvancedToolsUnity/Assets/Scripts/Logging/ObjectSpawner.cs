using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int startCount;
    [SerializeField] private int incrementCount;
    [SerializeField] private Vector3 offset;

    //vars
    [HideInInspector] public int totalCount;

    private void Awake()
    {
        for (int i = 0; i < startCount; i++) {
            CreateNextObject();
        }
    }

    //called by test reporter, creates the next 'step' context for the test
    public void NextStep()
    {
        for (int i = 0; i < incrementCount; i++) {
            CreateNextObject();
        }
    }

    //================ Object Creation ======================
    private void CreateNextObject()
    {
        Transform t = Instantiate(prefab, transform).transform;
        t.position = new Vector3(
            (totalCount % 10) * offset.x,                               //x position
            Mathf.FloorToInt((totalCount % 100) / 10f) * offset.y,      //y position
            Mathf.FloorToInt(totalCount / 100f) * offset.z              //z position
        );
        totalCount++;
    }
}
