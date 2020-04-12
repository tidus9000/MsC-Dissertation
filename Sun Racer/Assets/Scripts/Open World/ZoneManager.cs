using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    [SerializeField] string m_parameterName;
    [SerializeField] int m_scoreToBeat;
    bool activated = false;

    bool m_increaseVolume = false;
    [SerializeField] float m_volumeRate;
    float m_volume = 0;
    FMOD.Studio.EventInstance m_event;


    GameManager m_gm;
    GameObject m_player;

    // Start is called before the first frame update
    void Start()
    {
        m_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_player = GameObject.Find("Ship");
    }

    // Update is called once per frame
    void Update()
    {
        if (m_gm.m_score > m_scoreToBeat && activated)
        {
            ZoneEnd();
        }

        if (m_increaseVolume)
        {
            m_volume += m_volumeRate * Time.deltaTime;
            m_event.setParameterByName(m_parameterName, m_volume);
        }
    }

    void ZoneStart()
    {
        //Activate the BPM spawner
        //lock the controls
        m_player.GetComponentInChildren<SpawnerBPM>().enabled = true;
        m_player.GetComponentInChildren<Shipmovement>().m_openWorld = false;
        m_gm.m_score = 0;
        activated = true;
    }

    void ZoneEnd()
    {
        //deactivate the BPM Spawner
        //Unlock the controls
        //activate the parameter
        m_player.GetComponentInChildren<SpawnerBPM>().enabled = false;
        m_player.GetComponentInChildren<Shipmovement>().m_openWorld = true;
        m_event = GameObject.Find("MusicManager").GetComponent<MusicManager>().getMusicEvent();
        m_increaseVolume = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ZoneStart();
        }
    }
}
