using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenText : MonoBehaviour
{
    [SerializeField] Text m_interactionText;
    [SerializeField] string m_text;
    Text tempbox;

    // Start is called before the first frame update
    void Start()
    {
        tempbox = Instantiate(m_interactionText) as Text;
        tempbox.transform.SetParent(GameObject.Find("Canvas").transform);
        tempbox.text = m_text;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 textpos = Camera.main.WorldToScreenPoint(transform.position);
        if (textpos.z > 0)
        {
            tempbox.transform.position = textpos;
        }
        //tempbox.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
}
