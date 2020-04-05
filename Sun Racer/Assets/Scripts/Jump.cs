using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    GameManager m_gm;

    [SerializeField] float m_jumpHeight = 10;
    float m_jumpTime = 0;
    float m_maxJumpTime = 0;
    bool jumping = false;
    float startY = 0;

    FMOD.Studio.EventInstance m_musicEvent;

    // Start is called before the first frame update
    void Start()
    {
        m_musicEvent = GameObject.Find("MusicManager").GetComponent<MusicManager>().getMusicEvent();
        m_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_gm.getPaused())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerJump();
            }

            if (jumping)
            {
                //sin 0 to pi = 0 to 1 to 0
                //When jump time is max we want sin 0
                //When jump time is 0 we want sin pi

                float percentage = m_jumpTime / m_maxJumpTime;

                Vector3 newpos = transform.position;

                newpos.y = Mathf.Sin(Mathf.PI * percentage) * m_jumpHeight;


                //jumptime needs to start counting down to zero
                m_jumpTime -= Time.deltaTime;
                if (m_jumpTime <= 0)
                {
                    jumping = false;
                    newpos.y = startY;
                }


                //m_musicEvent.setParameterByName("JumpHeight", percentage);
                //float param = 100;
                //m_musicEvent.getParameterByName("Score", out param);
                //Debug.Log("Jump height: " + param);

                m_gm.m_jumpHeight = percentage;
                transform.position = newpos;
            }
        }
    }

    /// <summary>
    /// jumps for one bar of music
    /// </summary>
    [ContextMenu("Jump")]
    public void PlayerJump()
    {
        if (!jumping)
        {
            //Get the time of each beat, multiply by the beats per bar to get our jump time
            startY = transform.position.y;
            float timePerBeat = 60 / m_gm.m_bpm;
            m_jumpTime = m_gm.m_beatsPerBar * timePerBeat;
            m_maxJumpTime = m_jumpTime;
            jumping = true;
        }
    }
}
