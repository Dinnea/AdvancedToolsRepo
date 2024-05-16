using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataExporterCSV : MonoBehaviour
{
    public static string filename = "UnityPerformanceReport";
    private static string _filepath ="";

    private void Start()
    {
        _filepath = GetFilePath();
        Debug.Log(GetFilePath());
        generateLabels();
    }

    public static void WriteCSV_TEST(int iterations)
    {   
        TextWriter writer = new StreamWriter(_filepath, false);
        for (int i = 0; i < iterations; i++)
        {
            writer.WriteLine("ABC");
        }
        writer.Close();
    }

    public static void ExportResults(int objNumber)
    {
        TextWriter writer = new StreamWriter(GetFilePath(), true);
        writer.WriteLine(objNumber.ToString()+","+(960*objNumber).ToString());
        writer.Close();
        //Debug.Log("Line written.");
    }

    private void generateLabels()
    {
        TextWriter writer = new StreamWriter(GetFilePath(), false);
        writer.WriteLine("Objects, Triangles, Av. Frame Rate, CPU Usage, GPU Usage, RAM usage");
        writer.Close();
    }
    private static string GetFilePath()
    {
        return Application.persistentDataPath + "/" + filename + ".csv";
        //return Application.dataPath + "/" + filename + ".csv";
    }
}
