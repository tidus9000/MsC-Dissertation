using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
    public GameObject badShot;
	public Transform shotSpawn;
	public float fireRate;
	 
	private float nextFire;
    public bool m_canshoot = true;
    float m_timer;
    GameManager m_gameManager;
    RectTransform m_overheatBar;

    DataRecorder m_dr;

    [FMODUnity.EventRef] public string shootEvent;
    [FMODUnity.EventRef] public string badshootEvent;

    private void Start()
    {
        m_dr = GameObject.Find("DataRecorder").GetComponent<DataRecorder>();
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_overheatBar = GameObject.Find("OverheatBar").GetComponent<RectTransform>();
        Vector2 scale = m_overheatBar.localScale;
        scale.x = 0;
        m_overheatBar.localScale = scale;
    }

    void Update ()
	{
        if (Input.GetButtonDown("Fire1"))
        {
            m_dr.AddTiming(m_gameManager.m_playerBeatAccuracy, m_gameManager.m_inTime, m_gameManager.m_score);
        }

        if (Input.GetButton("Fire1") && Time.time > nextFire && m_canshoot) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            if (m_gameManager.m_inTime)
            {
                FMODUnity.RuntimeManager.PlayOneShot(shootEvent, transform.position);
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot(badshootEvent, transform.position);
                m_canshoot = false;
            }
		}

        //disable shooting for a bar
        if (!m_canshoot)
        {
            float freezetime = m_gameManager.m_beatsPerBar * m_gameManager.m_timePerBeat;
            m_timer += Time.deltaTime;
            //timeperbeat * beats per bar = 0, 0 = 3
            float percent = freezetime - m_timer;
            percent /= freezetime;
            Vector2 newScale = m_overheatBar.localScale;
            newScale.x = percent * 3;
            m_overheatBar.localScale = newScale;
            if (m_timer >= freezetime)
            {
                m_timer = 0;
                m_canshoot = true;
            }
        }
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
}
