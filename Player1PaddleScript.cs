using UnityEngine;
using System.Collections;

public class Player1PaddleScript : MonoBehaviour {

    private Vector3 offset;

    private GameObject player;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.Find("Player1");
        offset = player.transform.position - this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = player.transform.position - offset;
	}
}
