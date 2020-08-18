using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMarker : MonoBehaviour
{
    [SerializeField]float m_speed;
    [SerializeField]Vector2 m_target;
    MusicManager m_music;

    public bool selected = false;
    // Start is called before the first frame update
    void Start()
    {
        //transform.parent = GameObject.Find("Canvas").transform;

        //transform.SetParent(GameObject.Find("Canvas").transform, true);

        //when beat marker spawns, we want to set its speed so that it goes to the target after one bar
        m_target = GameObject.Find("BeatTargetPoint").transform.position;
        m_music = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        float beatsPerBar = m_music.timelineInfo.beatsPerBar;
        float timePerBeat = 60 / m_music.timelineInfo.tempo;
        float travelTime = timePerBeat * beatsPerBar;

        //speed = distance / time
        float distance = Vector2.Distance(transform.position, m_target);
        m_speed = distance / travelTime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newpos = Vector3.MoveTowards(transform.position, m_target, m_speed * Time.deltaTime);

        if (newpos.x == transform.position.x && newpos.y == transform.position.y)
        {
            Destroy(this.gameObject);
        }

        if (selected)
        {
            Debug.Log("Old pos: " + transform.position + " New Pos: " + newpos);
        }

        transform.position = newpos;

        
        if (transform.position.x == m_target.x && (transform.position.y <= m_target.y + 0.01 || transform.position.y >= m_target.y - 0.01))
        {
            Destroy(this.gameObject);
        }
    }
}
