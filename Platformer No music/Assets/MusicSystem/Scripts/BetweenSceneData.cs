using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenSceneData : MonoBehaviour
{
    public string m_sessionID;
    public string m_fileName;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
