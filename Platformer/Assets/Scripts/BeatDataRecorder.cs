using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BeatDataRecorder : MonoBehaviour
{
    List<float> beatDurations;
    List<float> intendedDurations;

    // Start is called before the first frame update
    void Start()
    {
        beatDurations = new List<float>();
        intendedDurations = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBeat(float _duration, float _intendedDuration)
    {
        beatDurations.Add(_duration);
        intendedDurations.Add(_intendedDuration);
    }

    public void SaveInfo()
    {
        string path = Application.streamingAssetsPath + "/BeatData.csv";

        StreamWriter writer = new StreamWriter(path);

        writer.WriteLine("Duration,Intended Duration");

        //if we have data to write, write it
        for (int i = 0; i < beatDurations.Count; i++)
        {
            writer.WriteLine(beatDurations[i].ToString() + "," + intendedDurations[i].ToString());
        }

        writer.Flush();
        writer.Close();

        Debug.Log("Saved beat data to: " + path);
    }
}
