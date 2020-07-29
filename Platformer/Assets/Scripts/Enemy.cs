using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float m_health = 20;
    [FMODUnity.EventRef] public string hitEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamageBlock"))
        {
            m_health -= collision.gameObject.GetComponent<Damageblock>().m_damage;
            Debug.Log("Enemy health now: " + m_health);
            FMODUnity.RuntimeManager.PlayOneShot(hitEvent, transform.position);
        }
    }
}
