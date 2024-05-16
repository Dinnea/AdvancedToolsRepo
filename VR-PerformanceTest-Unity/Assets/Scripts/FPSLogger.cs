using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLogger : MonoBehaviour
{
    private bool _isRunning = false;

    private float _totalTimeRecorded = 0;
    private int _totalFPS = 0;
    void Update()
    {
        if(_isRunning) updateFPS();
    }
    public void StartRecording()
    {
        //reset data
        _totalTimeRecorded = 0;
        _totalFPS = 0;

        _isRunning = true;
    }
    private void updateFPS()
    {
        _totalTimeRecorded += Time.deltaTime;
        _totalFPS++;
    }
    public void StopRecording()
    {
        _isRunning = false;
    }

    public float GetAverageFPS()
    {
        return _totalFPS / _totalTimeRecorded;
    }
    
}
