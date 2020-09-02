using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageblock : MonoBehaviour
{
    public int m_damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BlockBlock"))
        {
            Destroy(collision.gameObject);
            if (transform.parent.transform.parent.gameObject.GetComponent<Enemy>())
            {
                transform.parent.transform.parent.gameObject.GetComponent<Enemy>().Stunned();
            }
        }
    }
}
