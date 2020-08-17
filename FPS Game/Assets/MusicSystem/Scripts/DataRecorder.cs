using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataRecorder : MonoBehaviour
{
    struct data
    {
        public string sessionID;
        public List<float> timings;
        public List<bool> inTime;
        public List<int> score;
    }

    const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    data recordedData;
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void init()
    {
        string newid = "";
        //generate sessionID letters
        for (int i = 0; i < 4; i++)
        {
            newid += letters[Random.Range(0, letters.Length)];
        }

        newid += " - ";

        //generate session ID numbers
        for (int i = 0; i < 4; i++)
        {
            newid += Random.Range((int)0, (int)10).ToString();
        }

        recordedData.sessionID = newid;
        recordedData.timings = new List<float>();
        recordedData.inTime = new List<bool>();
        recordedData.score = new List<int>();
    }

    public void AddTiming(float _timing, bool _inTime, int _score)
    {
        recordedData.timings.Add(_timing);
        recordedData.inTime.Add(_inTime);
        recordedData.score.Add(_score);
    }

    public void SaveInfo()
    {
        string path = Application.streamingAssetsPath + "/data.csv";

        StreamWriter writer = new StreamWriter(path);

        writer.WriteLine("Timings,In Time,Score,SessionID");

        //if we have data to write, write it
        if (recordedData.timings.Count > 0)
        {
            writer.WriteLine(recordedData.timings[0] + ",,," + recordedData.sessionID);

            if (recordedData.timings.Count > 1)
            {
                for (int i = 1; i < recordedData.timings.Count; i++)
                {
                    writer.WriteLine(recordedData.timings[i] + ","
                        + recordedData.inTime[i].ToString() + ","
                        + recordedData.score[i]);
                }
            }
        }

        writer.Flush();
        writer.Close();

        Debug.Log("Saved data to: " + path);
    }
}
