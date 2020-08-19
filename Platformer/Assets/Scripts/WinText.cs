using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinText : MonoBehaviour
{
    BetweenSceneData m_bsd;
    TextMeshProUGUI m_text;

    // Start is called before the first frame update
    void Start()
    {
        m_bsd = GameObject.Find("BetweenSceneData").GetComponent<BetweenSceneData>();
        m_text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        m_text.text = "Thank you for playing. Your session ID is:\n" + m_bsd.m_sessionID +
            "\nYour play data has been saved to:\n" + Application.streamingAssetsPath + "\nUnder the name:\n" + m_bsd.m_fileName;
    }
}
