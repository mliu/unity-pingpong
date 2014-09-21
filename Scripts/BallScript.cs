using UnityEngine;
using System.Collections;
using System;

public class BallScript : MonoBehaviour {

    private float swingRange;

    private System.Random rnd = new System.Random();

    private float grav = 8f;

    private float yMax = -200f;

    private bool collided = false;

    private Vector3 velocity;

	// Use this for initialization
	void Start () {
        velocity = new Vector3(0f, 150f, 800f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate ()
    {
        velocity.y -= grav;
        if (velocity.y < yMax)
        {
            velocity.y = yMax;
        }
        this.transform.position += velocity/7000f;
    }

    void OnCollisionEnter (Collision col)
    {
        if (!collided)
        {
            if (col.gameObject.name == "Table")
            {
                velocity.y *= -1;
            }
            if (col.gameObject.name == "NetTable")
            {
                velocity.z *= -1;
            }
            if (col.gameObject.name == "Player1RSwing")
            {
                velocity.y = rnd.Next(160, 200);
                velocity.z *= -1;
                float temp = this.transform.position.x - col.transform.position.x;
                velocity.z = (velocity.z / Math.Abs(velocity.z)) * (400f + 70f * (-20 * temp * temp + 8));
                swingRange = rnd.Next(500, 600);
                velocity.x = 150 * temp / (swingRange / velocity.z);
            }
            if (col.gameObject.name == "Player1LSwing")
            {
                velocity.y = rnd.Next(130, 200);
                velocity.z *= -1;
                float temp = this.transform.position.x - col.transform.position.x;
                velocity.z = (velocity.z / Math.Abs(velocity.z)) * (400f + 70f * (-20 * temp * temp + 8));
                swingRange = rnd.Next(500, 600);
                velocity.x = 150 * temp / (swingRange / velocity.z);
            }
        }
        collided = true;
    }

    void OnCollisionExit ()
    {
        collided = false;
    }
}
