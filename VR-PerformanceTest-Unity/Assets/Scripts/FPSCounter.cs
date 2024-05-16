using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent (typeof(TextMeshProUGUI))]
public class FPSCounter : MonoBehaviour
{
    TextMeshProUGUI _fpsCounterText;
    [SerializeField] float _fpsTimer = 1;

    float _timer;

    private void Awake()
    {
       _fpsCounterText = GetComponent<TextMeshProUGUI>(); 
    }
    private void Update()
    {
        calculateFPS();
    }
    void calculateFPS()
    {
        if(Time.unscaledTime > _timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            _fpsCounterText.text = "FPS: " + fps.ToString();
            _timer = Time.unscaledTime + _fpsTimer;
        }
    }


    public static float OverTimeAverageFPS(float testTime)
    {
        float timer = 1;
        float fps = 0;
        for (int i = 0; i<testTime; i++)
        {
            if(Time.unscaledTime > timer)
            {
                fps += (int)(1f / Time.unscaledDeltaTime);
                timer = Time.unscaledTime + 1;
                //Debug.Log("Timed.");
                //Debug.Log
            }
        }
        //Debug.Log(fps/testTime);
        return fps/testTime;
    }
}
