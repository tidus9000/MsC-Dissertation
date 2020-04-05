using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHazardMiss : MonoBehaviour
{
    GameManager m_gm;

    // Start is called before the first frame update
    void Start()
    {
        m_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            GameObject.Find("GameManager").GetComponent<GameManager>().AddScore(1);
            m_gm.m_hazardsAvoided++;
        }
    }
}
