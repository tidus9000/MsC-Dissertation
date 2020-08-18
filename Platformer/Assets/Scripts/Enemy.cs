using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BeatActions
{
    GameManager m_gameManager;

    public float m_health = 20;
    PlayerLogic m_playerlog;
    bool m_attacking = false;
    float m_cantAttackTime = 0;
    GameObject m_player;


    public GameObject m_damageBlock;
    public Transform m_damagepointLeft;
    public Transform m_damagepointRight;
    Transform m_damagePoint;
    GameObject m_attackObject;
    bool m_attackZone = false;

    [Range(0,1)]public float m_chanceToAttack;

    float m_attackTimer = 0;
    [SerializeField] float m_attackTime;

    [FMODUnity.EventRef] public string hitEvent;
    [FMODUnity.EventRef] public string m_attackAudioEvent;

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_playerlog = m_player.GetComponent<PlayerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_health <= 0)
        {
            //Destroy(gameObject);
        }

        if (m_attacking)
        {
            m_attackTimer -= Time.deltaTime;
            if (m_attackTimer <= 0)
            {
                if (m_attackObject)
                {
                    Destroy(m_attackObject.gameObject);
                }
                m_attacking = false;
            }
        }

        if (m_cantAttackTime > 0)
        {
            m_cantAttackTime -= Time.deltaTime;
        }
        else if (m_cantAttackTime <= 0)
        {
            if (!m_attacking)
            {
                if (m_gameManager.m_timeToNextBeat <= 0.1)
                {
                    if (Random.Range(0.0f, 1.0f) <= m_chanceToAttack)
                    {
                        Debug.Log("Attacking");
                        attack();
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamageBlock"))
        {
            m_health -= collision.gameObject.GetComponent<Damageblock>().m_damage;
            //Debug.Log("Enemy health now: " + m_health);
            FMODUnity.RuntimeManager.PlayOneShot(hitEvent, transform.position);
            m_playerlog.AddCombo();
        }
    }

    void attack()
    {
        //Decide where to attack based on player location
        if (m_player.transform.position.x < transform.position.x)
        {
            m_damagePoint = m_damagepointLeft;
        }
        else
        {
            m_damagePoint = m_damagepointRight;
        }

        //spawn attack block
        m_attackObject = GameObject.Instantiate(m_damageBlock, m_damagePoint);

        //play attack sound
        FMODUnity.RuntimeManager.PlayOneShot(m_attackAudioEvent, m_damagePoint.position);

        //set attacking to true.
        m_attacking = true;

        //set the attack timer
        m_attackTimer = m_attackTime;
    }

    public void InAttackZone(bool _inZone)
    {
        m_attackZone = _inZone;
    }

    public override void NewBeat()
    {
        Debug.Log("Can attack now");
    }

    public void Stunned()
    {
        Debug.Log("I have been stunned");
        FMODUnity.RuntimeManager.PlayOneShot(hitEvent, transform.position);
        if (m_attackObject)
        {
            Destroy(m_attackObject.gameObject);
        }
        m_attacking = false;
        m_cantAttackTime = m_gameManager.m_timePerBeat * m_gameManager.m_beatsPerBar;
    }
}
