using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {
    private Rigidbody2D body;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
