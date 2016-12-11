using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.currentPlayer) || other.gameObject.Equals(Switcher.instance.prof))
        {
            Switcher.instance.KillPlayer(other.gameObject);
        } else if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ennemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
