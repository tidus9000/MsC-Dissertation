using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataTextHandler : MonoBehaviour
{
    BetweenSceneData m_bsd;
    TextMeshProUGUI m_text;

    // Start is called before the first frame update
    void Start()
    {
        m_bsd = GameObject.Find("BetweenSceneData").GetComponent<BetweenSceneData>();
        m_text = GetComponent<TextMeshProUGUI>();
        if (m_bsd)
        {
            m_text.text = ("Thank you for playing.\nYour session ID is:\n" + m_bsd.m_sessionID +
                "\n and has been saved to:\n" + Application.streamingAssetsPath + "\n under the file name:\n" + m_bsd.m_fileName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
