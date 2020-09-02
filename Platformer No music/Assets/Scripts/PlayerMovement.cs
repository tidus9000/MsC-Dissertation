using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_movementSpeed;
    [SerializeField] float m_jumpForce;
    bool m_jumping = false;

    GameManager m_gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        m_gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetAxis("Horizontal") != 0)
        {
            position.x += ((m_movementSpeed * Input.GetAxis("Horizontal")) * Time.deltaTime);
        }

        if (Input.GetAxis("Jump") != 0 && !m_jumping)
        {
            m_jumping = true;
            GetComponent<Rigidbody>().AddForce(0, m_jumpForce, 0);
        }

        transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Surface") && m_jumping)
        {
            m_jumping = false;
        }
    }
}
