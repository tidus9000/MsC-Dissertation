using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneManager : MonoBehaviour
{
    public float m_transitionTime = 1.0f;
    float m_transitionlevel = 0;
    bool gameover = false;
    bool win = false;
    bool transitionFinished = false;

    public string m_winScene;
    public string m_loseScene;

    DataRecorder m_dr;
    BetweenSceneData m_bsd;

    Image gameOverpanel;
    TextMeshPro gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        m_dr = GameObject.Find("GameManager").GetComponent<DataRecorder>();
        m_bsd = GameObject.Find("BetweenSceneData").GetComponent<BetweenSceneData>();
        gameOverpanel = GameObject.Find("GameOverPanel").GetComponent<Image>();
        gameOverText = gameOverpanel.gameObject.GetComponentInChildren<TextMeshPro>();
        gameOverpanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover)
        {
            gameOverpanel.gameObject.SetActive(true);
            //start the transition
            gameOverpanel.CrossFadeAlpha(255, m_transitionTime, false);
            m_transitionlevel += Time.deltaTime;
        }
        if (m_transitionlevel >= m_transitionTime)
        {
            if (win)
            {
                m_dr.SaveInfo();
                m_bsd.m_sessionID = m_dr.recordedData.sessionID;
                m_bsd.m_fileName = m_dr.m_saveName;
                SwitchScene(m_winScene);
            }else
            {
                m_dr.SaveInfo();
                m_bsd.m_sessionID = m_dr.recordedData.sessionID;
                m_bsd.m_fileName = m_dr.m_saveName;
                SwitchScene(m_loseScene);
            }
        }
    }

    public void GameOver(bool _win)
    {
        gameover = true;
        win = _win;
        Debug.Log("GameOver");
    }

    void SwitchScene(string _scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_scene);
    }
}
