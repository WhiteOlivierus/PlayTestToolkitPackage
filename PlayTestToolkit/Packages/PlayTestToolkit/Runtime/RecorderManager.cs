using DutchSkull.Singleton;
using PlayTestToolkit.Runtime.DataRecorders;
using System.Collections.Generic;
using UnityEngine;

public class RecorderManager : Singleton<RecorderManager>
{
    private readonly List<DataRecorder> recorders = new List<DataRecorder>();

    public void Awake()
    {
        InitialRecorder initialRecorder = new InitialRecorder("initial");

        initialRecorder.Record();
        initialRecorder.Save();

        recorders.Add(new InputRecorder("input", new List<KeyCode>()));

        string output = "Recording: \n";
        for (int i = 0; i < recorders.Count; i++)
            output = $"{output}    {recorders[i].GetType().Name} \n";

        Debug.Log(output);
    }

    public void Update()
    {
        for (int i = 0; i < recorders.Count; i++)
            recorders[i].Record();
    }

    public void OnApplicationQuit()
    {
        for (int i = 0; i < recorders.Count; i++)
            recorders[i].Save();
    }
}
