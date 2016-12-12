using UnityEngine;
using System.Collections;

public class WallCollider : MonoBehaviour {

    public bool walled;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Movable"))
        {
            walled = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Movable"))
        {
            walled = false;
        }
    }
}
