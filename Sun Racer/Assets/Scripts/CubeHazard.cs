using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHazard : MonoBehaviour
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
            GetComponent<AudioSource>().Play();
            GameObject.Find("GameManager").GetComponent<GameManager>().Gameover();
        }
    }
}
