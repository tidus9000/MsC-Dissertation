using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameObject m_damageblock;
    [SerializeField] Transform m_damagePoint;
    [SerializeField] int m_inTimeMultiplier = 3;
    GameManager m_gameManager;
    GameObject m_attack;

    [Range(0,1)] public float m_attackTime;
    float timer;

    [FMODUnity.EventRef] public string m_attackAudioEvent;
    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

            if (m_gameManager.m_inTime)
            {
                m_attack.GetComponent<Damageblock>().m_damage *= m_inTimeMultiplier;
                Debug.Log("Attacked in time");
                m_attack.GetComponent<MeshRenderer>().material.SetColor("_Colour", Color.red);
            }

            timer = m_attackTime;
        }

    }
}
