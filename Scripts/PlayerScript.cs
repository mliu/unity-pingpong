using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public Vector2 speed = new Vector2(5, 2);

    public GameObject ballPrefab;

    public bool served = false;

    private float tableCenter;

    private GameObject ball;

    private bool swingWait = false;

    private Vector2 movement;

    private GameObject paddleR;
    private GameObject paddleL;

	// Use this for initialization
	void Start ()
    {
        paddleR = GameObject.Find("Player1RSwing");
        paddleR.collider.enabled = false;
        paddleL = GameObject.Find("Player1LSwing");
        paddleL.collider.enabled = false;
        tableCenter = GameObject.Find("Table").transform.position.x;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

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
        movement = new Vector2(speed.x * inputX, speed.y * inputY);

        if (this.transform.position.x > tableCenter && this.transform.localScale.x != -5)
        {
            this.transform.localScale = new Vector3(-5, 5, 1);
        } else if (this.transform.position.x < tableCenter && this.transform.localScale.x != 5)
        {
            this.transform.localScale = new Vector3(5, 5, 1);
        }
	}

    void FixedUpdate()
    {
        rigidbody2D.velocity = movement;
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
