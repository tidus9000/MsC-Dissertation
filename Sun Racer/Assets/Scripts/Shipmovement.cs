using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shipmovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float m_maxTurnSpeed;
    [SerializeField] float m_maxTurnTilt;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.getPaused())
        {
            Vector3 pos = transform.position;
            Vector3 rot = transform.rotation.eulerAngles;

            if (Input.GetAxis("Horizontal") != 0)
            {
                pos.x += ((Input.GetAxis("Horizontal") * m_maxTurnSpeed) * Time.deltaTime);
                rot.z = ((Input.GetAxis("Horizontal") * m_maxTurnTilt)) * -1;
            }


            Vector3 forward = transform.forward;
            forward *= (speed * Time.deltaTime);
            pos += forward;

            transform.position = pos;
            transform.rotation = Quaternion.Euler(rot);
        }
    }

    public float getSpeed()
    {
        return speed;
    }
}
