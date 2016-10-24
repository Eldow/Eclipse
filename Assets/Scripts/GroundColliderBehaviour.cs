using UnityEngine;
using System.Collections;

public class GroundColliderBehaviour : MonoBehaviour {

    PlayerBehaviour parent;
    void Start()
    {
        parent = gameObject.GetComponentInParent<PlayerBehaviour>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!parent.grounded)
        {
            if (other.gameObject.tag == "Ground")
            {
                parent.grounded = true;
            }
        }

    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (parent.grounded)
        {
            if (other.gameObject.tag == "Ground")
            {
                parent.grounded = false;
            }
        }
    }
}
