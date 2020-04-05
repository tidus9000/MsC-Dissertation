using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHazard : MonoBehaviour
{
    GameManager m_gm;

    // Start is called before the first frame update
    void Start()
    {
        m_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_gm.m_numberOfHazards++;
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
            GameObject.Find("GameManager").GetComponent<GameManager>().Gameover();
        }

        if (other.CompareTag("Destroyer"))
        {
            m_gm.m_numberOfHazards--;
            Destroy(this.gameObject);
        }
    }
}
