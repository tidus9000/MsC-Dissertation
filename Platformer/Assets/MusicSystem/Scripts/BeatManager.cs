using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    float m_BPM = 0;
    [SerializeField]MusicManager m_musicMan;
    GameManager gm;
    Spawner m_sp;
    int currentBeat = 0;

    float m_durationOfCurrentBeat = 0;
    float m_durationOfPreviousBeat = 0;
    int beatsRecorded = 0;
    bool m_outofSync = false;
    BeatDataRecorder bdr;


    // Start is called before the first frame update
    void Start()
    {
        m_sp = GetComponent<Spawner>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_musicMan = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        bdr = GetComponent<BeatDataRecorder>();
        //m_BPM = m_musicMan.timelineInfo.tempo;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), beatsRecorded.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        m_durationOfCurrentBeat += Time.deltaTime;

        //this code is now commented out as I found it to be an innacurate way for managing beats.
        //Keeping it here for the purposes of the report
        if (currentBeat != m_musicMan.timelineInfo.currentBeat)
        {
            currentBeat = m_musicMan.timelineInfo.currentBeat;
            //spawn a beat marker
            m_sp.Spawn(transform.parent);
            //gm.m_timeToNextBeat = gm.m_timePerBeat;

            if (bdr)
            {
                bdr.AddBeat(m_durationOfCurrentBeat, gm.m_timePerBeat);
            }
            else
            {
                Debug.Log("Error, no beat data recorder found");
            }
            beatsRecorded++;
            m_durationOfPreviousBeat = m_durationOfCurrentBeat;
            m_durationOfCurrentBeat = 0;
        }

        //No idea if this will work but keeping both systems in place might
        if (gm.m_timeToNextBeat <= 0)
        {
            //spawn a beat marker
            gm.m_timeToNextBeat = gm.m_timePerBeat;
        }

        if (m_BPM != m_musicMan.timelineInfo.tempo)
        {
            m_BPM = m_musicMan.timelineInfo.tempo;
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (bdr)
            {
                bdr.SaveInfo();
            }
            else
            {
                Debug.Log("Error, no beat data recorder found");
            }
        }
    }
}
