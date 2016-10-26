using UnityEngine;
using System.Collections;

public class GroundColliderBehaviour : MonoBehaviour {
    public GameObject player;
    private PlayerBehaviour playerBehaviour;
    void Start()
    {
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
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
