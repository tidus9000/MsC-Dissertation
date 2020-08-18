using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_score = 0;
    bool m_paused = false;
    bool m_gameover = false;
    GameObject pausePanel;
    Text m_scoreText;

    //Variables to keep track of
    public float m_playerSpeed = 0;
    public float m_bpm = 0;
    public float m_jumpHeight = 0;
    public float m_beatsPerBar = 0;
    public float m_timeToNextBeat = 0;
    public float m_timePerBeat = 0;
    public int m_numberOfPickups = 0;
    public int m_numberOfHazards = 0;
    public float m_spawnDistance = 0;
    public int missedCollectables = 0;
    public int m_collecatblesPickedUp = 0;
    public int m_hazardsAvoided = 0;

    [Range(0, 0.25f)] public float m_beatZone;
    public bool m_inTime = false;
    public bool m_inExactTime = false;

    FMOD.Studio.EventInstance m_musicEvent;
    bool m_gotmusicEvent = false;

    public float m_playerBeatAccuracy = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        pausePanel = GameObject.Find("PausePanel");
        //m_scoreText = GameObject.Find("Score").GetComponent<Text>();
        //pausePanel.SetActive(false);
    }

    private void FixedUpdate()
    {
        m_timeToNextBeat -= Time.deltaTime;
        m_playerBeatAccuracy -= Time.deltaTime;
        if (m_playerBeatAccuracy <= 0 - m_beatZone || m_playerBeatAccuracy >= m_timeToNextBeat + m_beatZone)
        {
            m_playerBeatAccuracy = m_timeToNextBeat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_gotmusicEvent)
        {
            if (GameObject.Find("MusicManager").GetComponent<MusicManager>().getMusicEvent(ref m_musicEvent))
            {
                m_gotmusicEvent = true;
            }
            else
            {
                Debug.Log("Error getting music event");
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }

        if (!getPaused())
        {
            ManageJumping();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            DataRecorder dr = GetComponent<DataRecorder>();
            if (dr)
            {
                dr.SaveInfo();
            }
            else
            {
                Debug.Log("No Data recorder attached");
            }
        }

        m_timePerBeat = 60 / m_bpm;

        if (m_bpm != 0 && m_timeToNextBeat > m_timePerBeat)
        {
            m_timeToNextBeat = m_timePerBeat;
        }
        //work out if we're in time in ths frame
        if (m_timeToNextBeat <= m_beatZone || m_timeToNextBeat >= (m_timePerBeat - m_beatZone))
        {
            m_inTime = true;
        }
        else
        {
            m_inTime = false;
        }

        if (m_timeToNextBeat <= 0 || m_timeToNextBeat >= m_timePerBeat)
        {
            m_inExactTime = true;
        }
        else
        {
            m_inExactTime = false;
        }

        if (Input.GetMouseButtonDown(0) && m_inTime)
        {
            Debug.Log("Shot in time");
        }
        else if (Input.GetMouseButtonDown(0) && !m_inTime)
        {
            Debug.Log("shot out of time");
        }

        m_musicEvent.setParameterByName("Score", m_score);
        //m_musicEvent.getParameterByName("Score", out no);
        //Debug.Log("FMOD score: " + no);

        if (m_scoreText)
        {
            m_scoreText.text = "Score: " + m_score.ToString();
        }

    }



    //void OnGUI()
    //{
    //    GUILayout.Box(string.Format("Player Speed = {0} \n BPM = {1}\n Time to next beat = {2}\n" +
    //        " Number of pickups = {3}\n Number of hazards = {4}\n Spawn Distance = {5}\n" +
    //        "Missed collectables = {6}\n Collectables picked up = {7}\n Hazards Avoided = {8}"
    //        , m_playerSpeed, m_bpm, m_timeToNextBeat, m_numberOfPickups, m_numberOfHazards, m_spawnDistance
    //        , missedCollectables, m_collecatblesPickedUp, m_hazardsAvoided));
    //}

    public void Gameover()
    {
        m_gameover = true;
        PauseGame();
        pausePanel.GetComponentInChildren<Text>().text = "Game Over";
    }

    //pauses and unpauses game
    public void PauseGame()
    {
        m_paused = !m_paused;
        if (m_paused)
        {
            pausePanel.SetActive(true);
        }
        if (!m_paused && !m_gameover)
        {
            pausePanel.SetActive(false);
        }
        GameObject.Find("MusicManager").GetComponent<MusicManager>().pauseMusic();
    }

    public void AddScore(int _score)
    {
        m_score += _score;

        m_musicEvent.setParameterByName("Score", m_score);
        //m_musicEvent.getParameterByName("Score", out no);
        //Debug.Log("FMOD score: " + no);

        if (m_scoreText)
        {
            m_scoreText.text = "Score: " + m_score.ToString();
        }
    }

    void ManageJumping()
    {
        m_musicEvent.setParameterByName("JumpHeight", m_jumpHeight);
        float sc = 100;
        m_musicEvent.getParameterByName("JumpHeight", out sc);
    }

    public bool getPaused()
    {
        return m_paused;
    }
}
