using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public Vector3 speed = new Vector3(5, 0, 2);

    public GameObject ballPrefab;

    public bool served = false;

    private Vector3 tableCenter;
    private Vector3 initCenter;

    private float playerBoundingX = 2.8f;
    private float playerBoundingZ = .4f;

    private GameObject ball;

    private bool swingWait = false;

    private Vector3 movement;

    private GameObject paddleR;
    private GameObject paddleL;

	// Use this for initialization
	void Start ()
    {
        paddleR = GameObject.Find("Player1RSwing");
        paddleR.collider.enabled = false;
        paddleL = GameObject.Find("Player1LSwing");
        paddleL.collider.enabled = false;
        tableCenter = GameObject.Find("Table").transform.position;
        initCenter = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        if (Input.GetButton("SwingR"))
        {
            if (!served)
            {
                served = true;
                ball = (GameObject) Instantiate(ballPrefab, new Vector3(paddleR.transform.position.x, paddleR.transform.position.y, paddleR.transform.position.z + .3f), Quaternion.identity);
            }
            else
            {
                if (!swingWait)
                {
                    paddleR.collider.enabled = true;
                    swingWait = true;
                    Invoke("DisablePaddleCollider", 0.5f);
                    Invoke("SwingWait", 0.8f);
                }
            }
        } else if (Input.GetButton("SwingL"))
        {
            if (!served)
            {
                served = true;
                ball = (GameObject)Instantiate(ballPrefab, new Vector3(paddleL.transform.position.x, paddleL.transform.position.y, paddleL.transform.position.z + .3f), Quaternion.identity);
            }
            else
            {
                if (!swingWait)
                {
                    paddleL.collider.enabled = true;
                    swingWait = true;
                    Invoke("DisablePaddleCollider", 0.5f);
                    Invoke("SwingWait", 0.8f);
                }
            }
        }
        movement = new Vector3(speed.x * inputX, 0, speed.z * inputZ);

        if (this.transform.position.x > tableCenter.x && this.transform.localScale.x != -5)
        {
            this.transform.localScale = new Vector3(-5, 5, 1);
        } else if (this.transform.position.x < tableCenter.x && this.transform.localScale.x != 5)
        {
            this.transform.localScale = new Vector3(5, 5, 1);
        }
	}

    void FixedUpdate()
    {
        if (this.transform.position.x - tableCenter.x > playerBoundingX && movement.x > 0 || this.transform.position.x - tableCenter.x < -playerBoundingX && movement.x < 0)
        {
            movement.x = 0;
        }
        if (this.transform.position.z - initCenter.z > playerBoundingZ && movement.z > 0 || this.transform.position.z - initCenter.z < -playerBoundingZ && movement.z < 0)
        {
            movement.z = 0;
        }
        this.rigidbody.velocity = movement;
    }

    void DisablePaddleCollider()
    {
        paddleR.collider.enabled = false;
        paddleL.collider.enabled = false;
    }

    void SwingWait()
    {
        swingWait = false;
    }
}
