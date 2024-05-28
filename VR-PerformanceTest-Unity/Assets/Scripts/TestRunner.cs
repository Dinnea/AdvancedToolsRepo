using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.Profiling;

public class TestRunner : MonoBehaviour
{
    [SerializeField] MeshSpawner _spawner;
    [SerializeField] float _testDuration;
    List<TestParam> parameters;
    FPSLogger _fpsLogger;
    int _currentTest = 0;


    private void Start()
    {
       parameters=_spawner.GetParameterList();
        _fpsLogger = GetComponent<FPSLogger>();

        StartCoroutine(runTest(parameters[_currentTest]));
    }

    private void startTest()
    {
        TestParam currentParam = parameters[_currentTest];
        _spawner.Spawn(currentParam);
        //Data needed:
        //Obj count
        int objCount = currentParam.rows *currentParam.columns;
        //Triangles
        //Assumes ALWAYS the 960 triangle sphere TODO: automate that (?)
        int triangleCount = objCount * 960;
        //Average frame rate
        StartCoroutine(runTest(currentParam));
      

        //CPU usage
        //gpu usage
        //ram usage


    }
    private IEnumerator runTest(TestParam currentParam)
    {
        _spawner.Spawn(currentParam);
        yield return new WaitForSeconds(0.5f);
        //Data needed:
        //Obj count
        int objCount = currentParam.rows * currentParam.columns;
        //Triangles
        //Assumes ALWAYS the 960 triangle sphere TODO: automate that (?)
        int triangleCount = objCount * 960;
        //Average frame rate
        _fpsLogger.StartRecording();
        yield return new WaitForSeconds(_testDuration);
        _fpsLogger.StopRecording();
        UnityEngine.Debug.Log(_fpsLogger.GetAverageFPS());
        DataExporterCSV.ExportResults(objCount, _fpsLogger.GetAverageFPS());
        //CPU usage
        //Profiler.BeginSample()

        //gpu usage
        //ram usage

        _currentTest++;
        if(_currentTest<parameters.Count) StartCoroutine(runTest(parameters[_currentTest]));
        else Application.Quit();
    }
}
