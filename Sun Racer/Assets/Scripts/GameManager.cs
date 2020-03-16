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

    // Start is called before the first frame update
    void Start()
    {
        pausePanel = GameObject.Find("PausePanel");
        m_scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

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
        m_scoreText.text = "Score: " + m_score.ToString();
    }

    public bool getPaused()
    {
        return m_paused;
    }
}
