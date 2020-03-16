using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 pos = transform.position;
            Vector3 otherPos = other.transform.position;

            pos.x = otherPos.x;
            pos.z = otherPos.z;

            transform.position = pos;
        }
    }
}
