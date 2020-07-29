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


    // Start is called before the first frame update
    void Start()
    {
        m_sp = GetComponent<Spawner>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_musicMan = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        //m_BPM = m_musicMan.timelineInfo.tempo;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentBeat != m_musicMan.timelineInfo.currentBeat)
        {
            currentBeat = m_musicMan.timelineInfo.currentBeat;
            //spawn a beat marker
            m_sp.Spawn();
            gm.m_timeToNextBeat = gm.m_timePerBeat;
        }

        if (m_BPM != m_musicMan.timelineInfo.tempo)
        {
            m_BPM = m_musicMan.timelineInfo.tempo;
        }



        gm.m_timeToNextBeat -= Time.deltaTime;
    }
}
