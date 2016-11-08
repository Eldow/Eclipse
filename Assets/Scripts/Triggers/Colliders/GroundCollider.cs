using UnityEngine;
using System.Collections;

public class GroundCollider : MonoBehaviour {
    public bool grounded;

    void Start()
    {
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Movable"))
        {
            grounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Movable"))
        {
            grounded = false;
        }
    }
}
