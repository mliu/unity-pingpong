using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public Vector2 speed = new Vector2(5, 2);

    public GameObject ballPrefab;

    public bool served = false;

    private GameObject ball;

    private bool swingWait = false;

    private Vector2 movement;

    private Transform paddle;

	// Use this for initialization
	void Start ()
    {
        paddle = transform.Find("Player1Swing");
        paddle.collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if (Input.GetButton("Swing"))
        {
            if (!served)
            {
                served = true;
                ball = (GameObject) Instantiate(ballPrefab, new Vector3(paddle.position.x, paddle.position.y, paddle.position.z + .3f), Quaternion.identity);
            }
            else
            {
                if (!swingWait)
                {
                    paddle.collider.enabled = true;
                    swingWait = true;
                    Invoke("DisablePaddleCollider", 0.5f);
                    Invoke("SwingWait", 0.8f);
                }
            }
        }
        movement = new Vector2(speed.x * inputX, speed.y * inputY);
	}

    void FixedUpdate()
    {
        rigidbody2D.velocity = movement;
    }

    void DisablePaddleCollider()
    {
        paddle.collider.enabled = false;
    }

    void SwingWait()
    {
        swingWait = false;
    }
}
