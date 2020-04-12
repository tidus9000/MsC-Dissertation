using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Shipmovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float m_maxTurnSpeed;
    [SerializeField] float m_openworldTurnTilt;
    [SerializeField] float m_maxTurnTilt;

    public bool m_openWorld;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gm.m_playerSpeed = speed;
        if (!gm.getPaused())
        {
            Vector3 pos = transform.localPosition;
            Vector3 rot = transform.rotation.eulerAngles;

            if (Input.GetAxis("Horizontal") != 0)
            {
                if (m_openWorld)
                {
                    rot.y += ((Input.GetAxis("Horizontal") * m_openworldTurnTilt));
                    //pos.x += ((Input.GetAxis("Horizontal") * m_maxTurnSpeed) * Time.deltaTime);
                }
                else
                {
                    Vector3 right = transform.right;
                    right *= ((Input.GetAxis("Horizontal") * m_maxTurnSpeed) * Time.deltaTime);
                    //pos.x += ((Input.GetAxis("Horizontal") * m_maxTurnSpeed) * Time.deltaTime);
                    pos += right;
                    //rot.z = ((Input.GetAxis("Horizontal") * m_maxTurnTilt)) * -1;
                }
            }


            Vector3 forward = transform.forward;
            forward *= (speed * Time.deltaTime);
            pos += forward;

            transform.localPosition = pos;
            transform.rotation = Quaternion.Euler(rot);
        }
    }

    public float getSpeed()
    {
        return speed;
    }

    public float[] FindClosestObjects()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        List<Vector3> positions = new List<Vector3>();

        GameObject[] collectables = GameObject.FindGameObjectsWithTag("Pickup");
        GameObject[] hazards = GameObject.FindGameObjectsWithTag("Hazard");

        foreach( GameObject g in collectables)
        {
            positions.Add(g.transform.position);
        }

        foreach (GameObject g in hazards)
        {
            positions.Add(g.transform.position);
        }

        positions = positions.OrderBy(x => Vector3.Distance(player.transform.position, x)).ToList();

        List<float> distances = new List<float>();

        foreach(Vector3 p in positions)
        {
            distances.Add(Vector3.Distance(player.transform.position, p));
        }

        return distances.ToArray();
    }
}
