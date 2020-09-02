using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameObject m_damageblock;
    [SerializeField] GameObject m_blockBlock;
    [SerializeField] Transform m_damagePoint;
    [SerializeField] int m_inTimeMultiplier = 3;
    GameManager m_gameManager;
    GameObject m_attack;

    DataRecorder dr;

    [Range(0,1)] public float m_attackTime;
    float timer;

    [FMODUnity.EventRef] public string m_attackAudioEvent;
    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        dr = GameObject.Find("GameManager").GetComponent<DataRecorder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_attack)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(m_attack.gameObject);
            }
        }

        if (Input.GetAxis("Fire1") != 0 && m_attack == null)
        {
            m_attack = GameObject.Instantiate(m_damageblock, m_damagePoint);
            FMODUnity.RuntimeManager.PlayOneShot(m_attackAudioEvent, m_damagePoint.position);

            if (dr)
            {
                dr.AddTiming(m_gameManager.m_playerBeatAccuracy, m_gameManager.m_inTime, m_gameManager.m_score);
            }
            else
            {
                Debug.Log("No data recorder found when trying to add data");
            }

            m_attack.GetComponent<Damageblock>().m_damage *= m_inTimeMultiplier;

            timer = m_attackTime;
        }

        if (Input.GetAxis("Fire2") != 0 && m_attack == null)
        {

            if (dr)
            {
                dr.AddTiming(m_gameManager.m_playerBeatAccuracy, m_gameManager.m_inTime, m_gameManager.m_score);
            }
            else
            {
                Debug.Log("No data recorder found when trying to add data");
            }

            m_attack = GameObject.Instantiate(m_blockBlock, m_damagePoint);
            FMODUnity.RuntimeManager.PlayOneShot(m_attackAudioEvent, m_damagePoint.position);

            timer = m_attackTime;
        }

    }
}
