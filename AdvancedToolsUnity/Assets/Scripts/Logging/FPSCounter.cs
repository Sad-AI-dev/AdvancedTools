using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private int countedFrames = 0;

    private void Update()
    {
        countedFrames++;
    }

    // ================= Manage from external script =========================
    public int GetCountedFrames() { return countedFrames; }
    public void ResetCounter() { countedFrames = 0; }
}
