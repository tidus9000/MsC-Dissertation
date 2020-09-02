using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public int m_combo = 0;
    float m_comboTime;
    [SerializeField]float m_maxComboTime = 5;
    public int m_health = 100;

    GameManager m_gameManager;

    Text m_comboText;
    RectTransform m_comboBar;

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_comboText = GameObject.Find("ComboText").GetComponent<Text>();
        m_comboBar = GameObject.Find("ComboBar").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        m_gameManager.m_score = m_combo * 5;
        if (m_comboTime > 0)
        {
            m_comboTime -= Time.deltaTime;
            if (m_comboTime <= 0)
            {
                m_combo = 0;
            }
        }

        m_comboText.text = "Combo: " + m_combo;
        float percentage = m_comboTime / m_maxComboTime;
        Vector2 scale = m_comboBar.transform.localScale;
        scale.x = percentage;
        m_comboBar.localScale = scale;

        if (m_health <= 0)
        {
            Kill();
        }
    }

    public void AddCombo()
    {
        m_combo++;
        m_comboTime = m_maxComboTime;
    }

    public void Kill()
    {
        GameObject.Find("SceneManager").GetComponent<SceneManager>().GameOver(false);
    }
}
