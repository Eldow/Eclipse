using UnityEngine;
using System.Collections;

public class GroundColliderBehaviour : MonoBehaviour {
    private PlayerBehaviour playerBehaviour;
    void Start()
    {
        playerBehaviour = GetComponentInParent<PlayerBehaviour>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!playerBehaviour.grounded)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                playerBehaviour.grounded = true;
            }
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (playerBehaviour.grounded)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                playerBehaviour.grounded = false;
            }
        }
    }
}
