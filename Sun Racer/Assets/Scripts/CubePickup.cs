using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePickup : MonoBehaviour
{
    GameManager m_gm;

    // Start is called before the first frame update
    void Start()
    {
        m_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_gm.m_numberOfPickups++;
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
            GetComponent<Renderer>().enabled = false;
            m_gm.m_collecatblesPickedUp++;
        }

        if (other.CompareTag("Destroyer"))
        {
            m_gm.missedCollectables++;
            m_gm.m_numberOfPickups--;
            Destroy(this.gameObject);
        }
    }
}
