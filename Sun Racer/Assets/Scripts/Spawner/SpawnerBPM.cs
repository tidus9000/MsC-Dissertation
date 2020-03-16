using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class SpawnerBPM : MonoBehaviour
{
    [SerializeField] float m_BPM;
    float m_elapsedTime = 0;
    Spawner m_sp;
    MusicManager m_musicMan;
    GameManager gm;
    int currentBeat = 0;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_sp = GetComponent<Spawner>();
        m_musicMan = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
        m_BPM = m_musicMan.timelineInfo.tempo;
        adjustPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.getPaused())
        {
            if (currentBeat != m_musicMan.timelineInfo.currentBeat)
            {
                currentBeat = m_musicMan.timelineInfo.currentBeat;
                m_sp.SpawnRandom();
            }

            if (m_BPM != m_musicMan.timelineInfo.tempo)
            {
                m_BPM = m_musicMan.timelineInfo.tempo;
                adjustPosition();

            }
            //m_elapsedTime += Time.deltaTime;
            //if (m_elapsedTime >= m_interval)
            //{
            //    m_elapsedTime = 0;
            //    m_sp.SpawnRandom();
            //}
        }
    }

    //Spawner needs to know the position of the player, the speed of the player and the bpm so it can work out where to place itself
    //We wanr ro change local position along the Z axis
    void adjustPosition()
    {
        if (m_BPM <= 0)
        {
            Debug.Log("Trying to adjust spawn point with bpm of zero. don't do that");
            return;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        float playerspeed = GameObject.Find("Ship").GetComponent<Shipmovement>().getSpeed();
        Vector3 playerPos = playerObj.transform.localPosition;
        Vector3 newPos = Vector3.zero;
        newPos.z = (playerspeed * (60 / m_BPM)) * 2;
        transform.localPosition = newPos;
    }
}
