using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMarker : MonoBehaviour
{
    [SerializeField]float m_speed;
    [SerializeField]Vector2 m_target;
    MusicManager m_music;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = GameObject.Find("Canvas").transform;

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
        Vector2 newpos = Vector2.MoveTowards(transform.position, m_target, m_speed * Time.deltaTime);

        transform.position = newpos;

        if (transform.position.x == m_target.x && transform.position.y == m_target.y)
        {
            Destroy(this.gameObject);
        }
    }
}
