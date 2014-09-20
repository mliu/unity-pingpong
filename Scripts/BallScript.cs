using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

    private float grav = 5f;

    private float yMax = -200f;

    private bool collided = false;

    private Vector3 velocity;

	// Use this for initialization
	void Start () {
        velocity = new Vector3(0f, 100f, 800f);
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
        Debug.Log(velocity.z);
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
            if (col.gameObject.name == "Player1Swing")
            {
                velocity.z *= -1;
                float temp = col.transform.position.x - this.transform.position.x;
            }
        }
        collided = true;
    }

    void OnCollisionExit ()
    {
        collided = false;
    }
}
